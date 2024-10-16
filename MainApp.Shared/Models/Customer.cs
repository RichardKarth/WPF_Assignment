

namespace MainApp.Shared.Models;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DisplayName => $"{FirstName} {LastName}";

    public Product product { get; set; } = new Product();

}
