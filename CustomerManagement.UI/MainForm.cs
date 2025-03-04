// MainForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CustomerManagement.Business;
using CustomerManagement.Entities;

namespace CustomerManagement.UI
{
    public partial class MainForm : Form
    {
        private readonly CustomerManager _customerManager;
        private readonly CategoryManager _categoryManager;
        private List<Customer> _customerList;
        private List<Category> _categoryList;

        public MainForm()
        {
            InitializeComponent();
            
            // configure connection string - appsettings.json !!!
            string connectionString = "Server=.\\SQLEXPRESS;Database=CustomerManagement;Trusted_Connection=True;TrustServerCertificate=True;";
            _customerManager = new CustomerManager(connectionString);
            _categoryManager = new CategoryManager(connectionString);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadCustomers();
            SetupDataGridView();
        }

        private void LoadCategories()
        {
            try
            {
                _categoryList = _categoryManager.GetAllCategories();
                
                // ComboBox for adding Category
                cmbCategory.DataSource = null;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                cmbCategory.DataSource = new BindingList<Category>(_categoryList);

                // ComboBox for Filtering
                cmbFilterCategory.DataSource = null;
                var filterCategories = new List<Category>
                {
                    new Category { CategoryID = 0, CategoryName = "All Categories" }
                };
                filterCategories.AddRange(_categoryList);

                cmbFilterCategory.DisplayMember = "CategoryName";
                cmbFilterCategory.ValueMember = "CategoryID";
                cmbFilterCategory.DataSource = new BindingList<Category>(filterCategories);
                cmbFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
{
    try
    {
        // Verileri çek
        _customerList = _customerManager.GetAllCustomers();

        // Gelen veri sayısını kontrol et
        if (_customerList == null)
        {
            MessageBox.Show("Müşteri listesi boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_customerList.Count == 0)
        {
            MessageBox.Show("Herhangi bir müşteri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // DataGridView'a verileri ata
        dgvCustomers.DataSource = _customerList;

        // Yüklenen veri sayısını göster
        Text = $"Müşteri Yönetim Sistemi - Toplam Müşteri: {_customerList.Count}";
    }
    catch (Exception ex)
    {
        // Detaylı hata mesajı
        MessageBox.Show(
            $"Müşterileri yüklerken hata oluştu:\n\n" +
            $"Hata Mesajı: {ex.Message}\n" +
            $"Kaynak: {ex.Source}\n" +
            $"Yöntem: {ex.TargetSite}", 
            "Hata", 
            MessageBoxButtons.OK, 
            MessageBoxIcon.Error
        );
    }
}

        private void SetupDataGridView()
        {
            // DataGridView settings
            dgvCustomers.AutoGenerateColumns = false;
            
            // clear columns
            dgvCustomers.Columns.Clear();
            
            // add columns
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerCode",
                HeaderText = "Customer Code",
                Width = 100
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Customer Name",
                Width = 150
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category.CategoryName",
                HeaderText = "Category",
                Width = 100
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "E-mail",
                Width = 150
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Width = 100
            });
            
            // add buttons
            DataGridViewButtonColumn detailsButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Details",
                Text = "Details",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvCustomers.Columns.Add(detailsButtonColumn);

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvCustomers.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvCustomers.Columns.Add(deleteButtonColumn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                int? categoryId = null;
                
                if (cmbFilterCategory.SelectedIndex > 0)
                {
                    categoryId = ((Category)cmbFilterCategory.SelectedItem).CategoryID;
                }
                
                _customerList = _customerManager.SearchCustomers(searchTerm, categoryId);
                dgvCustomers.DataSource = _customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer
                {
                    CustomerCode = txtCustomerCode.Text,
                    CustomerName = txtCustomerName.Text,
                    CustomerCategory = (int)cmbCategory.SelectedValue,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text
                };

                bool success = _customerManager.AddCustomer(newCustomer);
                
                if (success)
                {
                    MessageBox.Show("Customer successfully added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("An error occurred while adding the customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            using (NewCategoryForm categoryForm = new NewCategoryForm(_categoryManager))
            {
                if (categoryForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories();
                }
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If the clicked cell is in the button column and has a valid row index
            if (e.RowIndex >= 0)
            {
                Customer selectedCustomer = _customerList[e.RowIndex];

                // Details button (6th column)
                if (e.ColumnIndex == 6)
                {
                    ShowCustomerDetails(selectedCustomer);
                }
                // Edit button (7th column)
                else if (e.ColumnIndex == 7)
                {
                    EditCustomer(selectedCustomer);
                }
                // Delete button (8th column)
                else if (e.ColumnIndex == 8)
                {
                    DeleteCustomer(selectedCustomer);
                }
            }
        }

        private void ShowCustomerDetails(Customer customer)
        {
            MessageBox.Show(
                $"Customer ID: {customer.CustomerID}\n" +
                $"Customer Code: {customer.CustomerCode}\n" +
                $"Customer Name: {customer.CustomerName}\n" +
                $"Category Name: {customer.Category.CategoryName}\n" +
                $"Category Description: {customer.Category.CategoryDescription}\n" +
                $"Email: {customer.Email}\n" +
                $"Phone: {customer.Phone}\n" +
                $"Created Date: {customer.CreatedDate}\n" +
                $"Update Date: {customer.UpdateDate}",
                "Customer Details",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void EditCustomer(Customer customer)
        {
            using (EditCustomerForm editForm = new EditCustomerForm(_customerManager, _categoryList, customer))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
        }

        private void DeleteCustomer(Customer customer)
        {
            if (MessageBox.Show($"Are you sure you want to delete the customer {customer.CustomerName}?", "Deletion Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool success = _customerManager.DeleteCustomer(customer.CustomerID);
                    
                    if (success)
                    {
                        MessageBox.Show("Customer successfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomers();
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while deleting the customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
        }
    }
}