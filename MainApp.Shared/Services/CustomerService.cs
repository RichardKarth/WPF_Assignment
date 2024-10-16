using MainApp.Shared.Enums;
using MainApp.Shared.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MainApp.Shared.Services;

public class CustomerService : ICustomerService
{
    public Customer? CurrentCustomer { get; set; } 
    private List<Customer> _customerList;
    private readonly IFileService _fileService;


    public CustomerService(IFileService fileService)
    {
        _fileService = fileService;
        _customerList = new List<Customer>();
        
    }

    public StatusCodes SaveCustomer(Customer customer)
    {
        if (_customerList.Any(x => x.Email == customer.Email))
        {
            return StatusCodes.Exists;
        }
        try
        {
            _customerList.Add(customer);
            var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_customerList, Formatting.Indented));
            return StatusCodes.Success;
        }
        catch (Exception ex)
        {
            return StatusCodes.Failed;
        }
    }
    public StatusCodes Update(Customer customer)
    {
        var existingCustomer = _customerList.FirstOrDefault(x => x.Id == customer.Id);
        if (existingCustomer == null)
        {
            return StatusCodes.NotFound;
        }
        try
        {
            existingCustomer = customer;
            var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_customerList, Formatting.Indented));
            return StatusCodes.Success;
        }
        catch (Exception ex)
        {
            return StatusCodes.Failed;
        }
    }
    public IEnumerable<Customer> GetCustomer()
    {
        try
        {
            string content = _fileService.GetFromFile();

            if (!string.IsNullOrEmpty(content))
            {
                var tempList = JsonConvert.DeserializeObject<List<Customer>>(content)!;
                if (tempList != null && tempList.Count > 0)
                {
                    _customerList = tempList;
                }
            }
        }
        catch { }
        return _customerList;
    }

    public StatusCodes Delete(string id)
    {
        try
        {
            _customerList = GetCustomer().ToList();

            var customer = _customerList.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                _customerList.Remove(customer);
                var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_customerList, Formatting.Indented));
                _customerList = GetCustomer().ToList();
                return StatusCodes.Success;
            }
            else
            {
                return StatusCodes.NotFound;
            }
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }
}
