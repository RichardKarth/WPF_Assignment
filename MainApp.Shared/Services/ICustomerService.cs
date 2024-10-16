using MainApp.Shared.Enums;
using MainApp.Shared.Models;

namespace MainApp.Shared.Services
{
    public interface ICustomerService
    {
        Customer? CurrentCustomer { get; set; }

        IEnumerable<Customer> GetCustomer();
        StatusCodes SaveCustomer(Customer customer);
        StatusCodes Delete(string id);
        StatusCodes Update(Customer customer);
    }
}