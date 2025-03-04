// Customer.cs
using System;

namespace CustomerManagement.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CustomerCategory { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        
        // Navigation property
        public Category Category { get; set; }


        // Validation metodları eklenebilir
    public bool IsValidEmail()
    {
        try 
        {
            var addr = new System.Net.Mail.MailAddress(Email);
            return addr.Address == Email;
        }
        catch
        {
            return false;
        }
    }

    public bool IsValidPhoneNumber()
    {
        // Basit telefon numarası kontrolü
        return !string.IsNullOrWhiteSpace(Phone) && 
               Phone.Length >= 10 && 
               Phone.Length <= 15;
    }
    }
}