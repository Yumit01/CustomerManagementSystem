using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CustomerManagement.Business;
using CustomerManagement.Entities;

namespace CustomerManagement.UI
{
    public partial class EditCustomerForm : Form
    {
        private readonly CustomerManager _customerManager;
        private readonly Customer _customer;

        public EditCustomerForm(CustomerManager customerManager, List<Category> categories, Customer customer)
        {
            InitializeComponent();
            _customerManager = customerManager;
            _customer = customer;

            // Fill ComboBox
            cmbCategory.DataSource = null;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DataSource = categories;

            // Fill customer information into form fields
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            txtCustomerId.Text = _customer.CustomerID.ToString();
            txtCustomerCode.Text = _customer.CustomerCode;
            txtCustomerName.Text = _customer.CustomerName;
            txtEmail.Text = _customer.Email;
            txtPhone.Text = _customer.Phone;
            cmbCategory.SelectedValue = _customer.CustomerCategory;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    MessageBox.Show("Customer name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update customer object
                _customer.CustomerCode = txtCustomerCode.Text;
                _customer.CustomerName = txtCustomerName.Text;
                _customer.CustomerCategory = (int)cmbCategory.SelectedValue;
                _customer.Email = txtEmail.Text;
                _customer.Phone = txtPhone.Text;

                bool success = _customerManager.UpdateCustomer(_customer);
                
                if (success)
                {
                    MessageBox.Show("Customer successfully updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("An error occurred while updating the customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Get user exit confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to exit without saving changes?", 
                "Exit Confirmation", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Close the form without any changes
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}