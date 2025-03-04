// ICustomerRepository.cs
using CustomerManagement.Entities;

public interface ICustomerRepository
{
    List<Customer> GetAll();
    Customer GetById(int id);
    int Add(Customer customer);
    bool Update(Customer customer);
    bool Delete(int id);
    List<Customer> Search(string term, int? categoryId);
}