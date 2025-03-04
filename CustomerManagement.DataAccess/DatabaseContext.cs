// DatabaseContext.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CustomerManagement.Entities;

namespace CustomerManagement.DataAccess
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Customer Methods
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT c.*, cat.CategoryName, cat.CategoryDescription 
                               FROM Customers c
                               LEFT JOIN Categories cat ON c.CustomerCategory = cat.CategoryID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerCode = reader["CustomerCode"].ToString(),
                            CustomerName = reader["CustomerName"].ToString(),
                            CustomerCategory = Convert.ToInt32(reader["CustomerCategory"]),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                            Category = new Category
                            {
                                CategoryID = Convert.ToInt32(reader["CustomerCategory"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                CategoryDescription = reader["CategoryDescription"].ToString()
                            }
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT c.*, cat.CategoryName, cat.CategoryDescription 
                               FROM Customers c
                               LEFT JOIN Categories cat ON c.CustomerCategory = cat.CategoryID
                               WHERE c.CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerCode = reader["CustomerCode"].ToString(),
                            CustomerName = reader["CustomerName"].ToString(),
                            CustomerCategory = Convert.ToInt32(reader["CustomerCategory"]),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                            Category = new Category
                            {
                                CategoryID = Convert.ToInt32(reader["CustomerCategory"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                CategoryDescription = reader["CategoryDescription"].ToString()
                            }
                        };
                    }
                }
            }

            return customer;
        }

        public List<Customer> SearchCustomers(string searchTerm, int? categoryId = null)
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT c.*, cat.CategoryName, cat.CategoryDescription 
                               FROM Customers c
                               LEFT JOIN Categories cat ON c.CustomerCategory = cat.CategoryID
                               WHERE (c.CustomerName LIKE @SearchTerm OR 
                                      c.CustomerCode LIKE @SearchTerm OR 
                                      c.Email LIKE @SearchTerm OR 
                                      c.Phone LIKE @SearchTerm)";

                if (categoryId.HasValue)
                {
                    query += " AND c.CustomerCategory = @CategoryID";
                }

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                
                if (categoryId.HasValue)
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryId.Value);
                }

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerCode = reader["CustomerCode"].ToString(),
                            CustomerName = reader["CustomerName"].ToString(),
                            CustomerCategory = Convert.ToInt32(reader["CustomerCategory"]),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                            Category = new Category
                            {
                                CategoryID = Convert.ToInt32(reader["CustomerCategory"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                CategoryDescription = reader["CategoryDescription"].ToString()
                            }
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public int AddCustomer(Customer customer)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        try
        {
            // Validate category exists
            using (SqlCommand checkCategoryCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Categories WHERE CategoryID = @CategoryID", connection))
            {
                checkCategoryCmd.Parameters.AddWithValue("@CategoryID", customer.CustomerCategory);
                connection.Open();
                int categoryCount = (int)checkCategoryCmd.ExecuteScalar();
                
                if (categoryCount == 0)
                {
                    throw new Exception($"Kategori ID {customer.CustomerCategory} bulunamadı.");
                }
            }

            // Prepared SQL with detailed parameter handling
            string query = @"
                INSERT INTO Customers 
                (CustomerCode, CustomerName, CustomerCategory, Email, Phone, CreatedDate, UpdateDate)
                VALUES 
                (@CustomerCode, @CustomerName, @CustomerCategory, @Email, @Phone, @CreatedDate, @UpdateDate);
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters with null handling
                command.Parameters.AddWithValue("@CustomerCode", 
                    string.IsNullOrWhiteSpace(customer.CustomerCode) 
                    ? (object)DBNull.Value 
                    : customer.CustomerCode);
                
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@CustomerCategory", customer.CustomerCategory);
                
                command.Parameters.AddWithValue("@Email", 
                    string.IsNullOrWhiteSpace(customer.Email) 
                    ? (object)DBNull.Value 
                    : customer.Email);
                
                command.Parameters.AddWithValue("@Phone", 
                    string.IsNullOrWhiteSpace(customer.Phone) 
                    ? (object)DBNull.Value 
                    : customer.Phone);
                
                command.Parameters.AddWithValue("@CreatedDate", customer.CreatedDate);
                command.Parameters.AddWithValue("@UpdateDate", customer.UpdateDate);

                // Execute and return the new ID
                object result = command.ExecuteScalar();
                
                if (result == null)
                {
                    throw new Exception("Müşteri eklenemedi.");
                }

                return Convert.ToInt32(result);
            }
        }
        catch (SqlException sqlEx)
        {
            // Log SQL specific errors
            Console.WriteLine($"SQL Error: {sqlEx.Number}, Message: {sqlEx.Message}");
            throw; // Re-throw to be caught by caller
        }
        catch (Exception ex)
        {
            // Log general errors
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }
}

        public bool UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Customers
                               SET CustomerCode = @CustomerCode,
                                   CustomerName = @CustomerName,
                                   CustomerCategory = @CustomerCategory,
                                   Email = @Email,
                                   Phone = @Phone,
                                   UpdateDate = @UpdateDate
                               WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@CustomerCategory", customer.CustomerCategory);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@UpdateDate", DateTime.Now);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        // Category Methods
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            CategoryDescription = reader["CategoryDescription"].ToString()
                        };

                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public int AddCategory(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Categories (CategoryName, CategoryDescription)
                               VALUES (@CategoryName, @CategoryDescription);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);

                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public bool IsCategoryInUse(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE CustomerCategory = @CategoryID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryID", categoryId);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}