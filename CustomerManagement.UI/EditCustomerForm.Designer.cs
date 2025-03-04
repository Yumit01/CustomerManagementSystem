using System;
using System.Windows.Forms;

namespace CustomerManagement.UI
{
    public partial class EditCustomerForm : Form
    {
        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Edit Customer";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Customer ID Label and TextBox
            Label lblCustomerId = new Label
            {
                Text = "Customer ID:",
                Location = new System.Drawing.Point(20, 20),
                Width = 150
            };

            txtCustomerId = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Width = 200,
                ReadOnly = true
            };

            // Customer Code Label and TextBox
            Label lblCustomerCode = new Label
            {
                Text = "Customer Code:",
                Location = new System.Drawing.Point(20, 90),
                Width = 150
            };

            txtCustomerCode = new TextBox
            {
                Location = new System.Drawing.Point(20, 120),
                Width = 200,
                PlaceholderText = "Enter customer code"
            };

            // Customer Name Label and TextBox
            Label lblCustomerName = new Label
            {
                Text = "Customer Name:",
                Location = new System.Drawing.Point(20, 160),
                Width = 150
            };

            txtCustomerName = new TextBox
            {
                Location = new System.Drawing.Point(20, 190),
                Width = 200,
                PlaceholderText = "Enter customer name"
            };

            // Category Label and ComboBox
            Label lblCategory = new Label
            {
                Text = "Category:",
                Location = new System.Drawing.Point(250, 20),
                Width = 150
            };

            cmbCategory = new ComboBox
            {
                Location = new System.Drawing.Point(250, 50),
                Width = 200
            };

            // Email Label and TextBox
            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(250, 90),
                Width = 150
            };

            txtEmail = new TextBox
            {
                Location = new System.Drawing.Point(250, 120),
                Width = 200,
                PlaceholderText = "Enter email address"
            };

            // Phone Label and TextBox
            Label lblPhone = new Label
            {
                Text = "Phone:",
                Location = new System.Drawing.Point(250, 160),
                Width = 150
            };

            txtPhone = new TextBox
            {
                Location = new System.Drawing.Point(250, 190),
                Width = 200,
                PlaceholderText = "Enter phone number"
            };

            // Save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(250, 250),
                Width = 100
            };
            btnSave.Click += btnSave_Click;

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(360, 250),
                Width = 100
            };
            btnCancel.Click += btnCancel_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblCustomerId, txtCustomerId,
                lblCustomerCode, txtCustomerCode,
                lblCustomerName, txtCustomerName,
                lblCategory, cmbCategory,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                btnSave, btnCancel
            });
        }

        // Form controls
        private TextBox txtCustomerId;
        private TextBox txtCustomerCode;
        private TextBox txtCustomerName;
        private ComboBox cmbCategory;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnSave;
        private Button btnCancel;
    }
}