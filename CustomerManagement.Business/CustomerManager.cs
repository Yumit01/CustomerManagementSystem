using System;
using System.Collections.Generic;
using CustomerManagement.DataAccess;
using CustomerManagement.Entities;

namespace CustomerManagement.Business
{
    public class CustomerManager
    {
        private readonly DatabaseContext _dbContext;

        public CustomerManager(string connectionString)
        {
            _dbContext = new DatabaseContext(connectionString);
        }

        public List<Customer> GetAllCustomers()
        {
            return _dbContext.GetAllCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _dbContext.GetCustomerById(customerId);
        }

        public List<Customer> SearchCustomers(string searchTerm, int? categoryId = null)
        {
            return _dbContext.SearchCustomers(searchTerm, categoryId);
        }

        public bool AddCustomer(Customer customer)
        {
            try
            {
                // Comprehensive validation
                if (string.IsNullOrWhiteSpace(customer.CustomerName))
                    throw new ArgumentException("Customer name cannot be empty.");

                if (customer.CustomerCategory <= 0)
                    throw new ArgumentException("A valid category must be selected.");

                // Validate email if provided
                if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
                    throw new ArgumentException("Invalid email format.");

                // Set default values if not provided
                customer.CustomerCode = string.IsNullOrWhiteSpace(customer.CustomerCode) 
                    ? GenerateCustomerCode() 
                    : customer.CustomerCode;

                // Set default dates
                customer.CreatedDate = DateTime.Now;
                customer.UpdateDate = DateTime.Now;

                // Debug logging
                Console.WriteLine($"Adding Customer: {customer.CustomerName}");
                Console.WriteLine($"Customer Category: {customer.CustomerCategory}");
                Console.WriteLine($"Customer Code: {customer.CustomerCode}");

                int newId = _dbContext.AddCustomer(customer);
                
                Console.WriteLine($"New Customer ID: {newId}");
                
                return newId > 0;
            }
            catch (Exception ex)
            {
                // Comprehensive error logging
                Console.WriteLine($"Customer Add Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                
                // Consider logging to a file in a real application
                throw; // Re-throw to allow UI to catch and display
            }
        }

        // Email validation method
        private bool IsValidEmail(string email)
        {
            try 
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }

        // Customer code generation
        private string GenerateCustomerCode()
        {
            return $"CUST-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customer.CustomerName))
                    throw new ArgumentException("Customer name cannot be empty.");

                return _dbContext.UpdateCustomer(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                return _dbContext.DeleteCustomer(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}