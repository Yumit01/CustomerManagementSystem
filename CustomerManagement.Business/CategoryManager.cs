using System;
using System.Collections.Generic;
using CustomerManagement.DataAccess;
using CustomerManagement.Entities;

namespace CustomerManagement.Business
{
    public class CategoryManager
    {
        private readonly DatabaseContext _dbContext;

        public CategoryManager(string connectionString)
        {
            _dbContext = new DatabaseContext(connectionString);
        }

        public List<Category> GetAllCategories()
        {
            return _dbContext.GetAllCategories();
        }

        public bool AddCategory(Category category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                    throw new ArgumentException("Category name cannot be empty.");

                int newId = _dbContext.AddCategory(category);
                return newId > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CanDeleteCategory(int categoryId)
        {
            return !_dbContext.IsCategoryInUse(categoryId);
        }
    }
}