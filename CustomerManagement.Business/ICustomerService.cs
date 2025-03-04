// ICustomerService.cs
using CustomerManagement.Entities;

public interface ICustomerService
{
    List<Customer> GetCustomers();
    Customer GetCustomerDetails(int id);
    bool CreateCustomer(Customer customer);
    bool UpdateCustomerInformation(Customer customer);
    bool RemoveCustomer(int customerId);
    bool ValidateCustomer(Customer customer);
}