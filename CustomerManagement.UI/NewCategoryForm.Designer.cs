using System;
using System.Windows.Forms;

namespace CustomerManagement.UI
{
    public partial class NewCategoryForm : Form
    {
        private void InitializeComponent()
        {
            // setting up the form
            this.Text = "Add New Category";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Size = new System.Drawing.Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            // category name Label
            Label lblCategoryName = new Label
            {
                Text = "Category Name:",
                Location = new System.Drawing.Point(20, 20),
                Width = 150
            };

            // category name TextBox
            txtCategoryName = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Width = 350,
                PlaceholderText = "Insert Category Name"
            };

            // category description Label
            Label lblCategoryDescription = new Label
            {
                Text = "Category Description:",
                Location = new System.Drawing.Point(20, 90),
                Width = 150
            };

            // category description TextBox
            txtCategoryDescription = new TextBox
            {
                Location = new System.Drawing.Point(20, 120),
                Width = 350,
                Height = 80,
                Multiline = true
            };

            // save Button
            btnSave = new Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(210, 220),
                Width = 80
            };
            btnSave.Click += btnSave_Click;

            // cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(290, 220),
                Width = 80
            };
            btnCancel.Click += btnCancel_Click;

            // adding controls to the form
            this.Controls.AddRange(new Control[] {
                lblCategoryName,
                txtCategoryName,
                lblCategoryDescription,
                txtCategoryDescription,
                btnSave,
                btnCancel
            });
        }

        // private fields
        private TextBox txtCategoryName;
        private TextBox txtCategoryDescription;
        private Button btnSave;
        private Button btnCancel;
    }
}