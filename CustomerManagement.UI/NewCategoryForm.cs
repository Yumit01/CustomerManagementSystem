// NewCategoryForm.cs
using System;
using System.Windows.Forms;
using CustomerManagement.Business;
using CustomerManagement.Entities;

namespace CustomerManagement.UI
{
    public partial class NewCategoryForm : Form
    {
        private readonly CategoryManager _categoryManager;

        public NewCategoryForm(CategoryManager categoryManager)
        {
            InitializeComponent();
            _categoryManager = categoryManager;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryName.Text))
                {
                    MessageBox.Show("Category name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Category newCategory = new Category
                {
                    CategoryName = txtCategoryName.Text,
                    CategoryDescription = txtCategoryDescription.Text
                };

                bool success = _categoryManager.AddCategory(newCategory);
                
                if (success)
                {
                    MessageBox.Show("Category successfully added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("An error occurred while adding the category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}