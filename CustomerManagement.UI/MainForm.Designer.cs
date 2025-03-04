namespace CustomerManagement.UI;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    // private void InitializeComponent()
    // {
    //     this.components = new System.ComponentModel.Container();
    //     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //     this.ClientSize = new System.Drawing.Size(800, 450);
    //     this.Text = "Form1";
    // }

    private void InitializeComponent()
        {
            this.Text = "Customer Management System";
            this.ClientSize = new System.Drawing.Size(1000, 600);
            
            // search Panel
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100
            };

            // search TextBox
            txtSearch = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 200,
                PlaceholderText = "Search Customer..."
            };
            searchPanel.Controls.Add(txtSearch);

            // category Filter ComboBox
            cmbFilterCategory = new ComboBox
            {
                Location = new System.Drawing.Point(220, 10),
                Width = 150
            };
            searchPanel.Controls.Add(cmbFilterCategory);

            // seach Button
            btnSearch = new Button
            {
                Text = "Search",
                Location = new System.Drawing.Point(380, 10),
                Width = 100
            };
            btnSearch.Click += btnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            // add Customer Panel
            Panel addCustomerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150
            };

            // customer Code TextBox
            txtCustomerCode = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                PlaceholderText = "Customer Code"
            };
            addCustomerPanel.Controls.Add(txtCustomerCode);

            txtCustomerName = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                PlaceholderText = "Customer Name"
            };
            addCustomerPanel.Controls.Add(txtCustomerName);

            // e-Mail TextBox
            txtEmail = new TextBox
            {
                Location = new System.Drawing.Point(10, 70),
                PlaceholderText = "E-mail"
            };
            addCustomerPanel.Controls.Add(txtEmail);

            // phone TextBox
            txtPhone = new TextBox
            {
                Location = new System.Drawing.Point(10, 100),
                PlaceholderText = "Phone"
            };
            addCustomerPanel.Controls.Add(txtPhone);

            // category ComboBox
            cmbCategory = new ComboBox
            {
                Location = new System.Drawing.Point(220, 10),
                Width = 150
            };
            addCustomerPanel.Controls.Add(cmbCategory);

            // new Category Button
            btnNewCategory = new Button
            {
                Text = "New Category",
                Location = new System.Drawing.Point(380, 10)
            };
            btnNewCategory.Click += btnNewCategory_Click;
            addCustomerPanel.Controls.Add(btnNewCategory);

            // customer Add Button
            btnAddCustomer = new Button
            {
                Text = "Add Customer",
                Location = new System.Drawing.Point(380, 40)
            };
            btnAddCustomer.Click += btnAddCustomer_Click;
            addCustomerPanel.Controls.Add(btnAddCustomer);

            // DataGridView
            dgvCustomers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // add controls to form
            this.Controls.Add(dgvCustomers);
            this.Controls.Add(addCustomerPanel);
            this.Controls.Add(searchPanel);
        }

        // private members
        private TextBox txtSearch;
        private ComboBox cmbFilterCategory;
        private Button btnSearch;
        private TextBox txtCustomerCode;
        private TextBox txtCustomerName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private ComboBox cmbCategory;
        private Button btnNewCategory;
        private Button btnAddCustomer;
        private DataGridView dgvCustomers;
        
        #endregion
}
