using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;



namespace Angelina_Expense_Tracker_App_Data_Access.Models
{
    public class ExpenseModel
    {
        public int ExpenseID { get; set; }
        
        
        [Required(ErrorMessage = "Expense Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters")]
        public string Category { get; set; }
        public string Location { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Amount must be greater than zero")]
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; } 
        public DateTime? Date { get; set; } 
        public string? Notes { get; set; }

        public ExpenseModel(int expenseID, string name, string category, string location, decimal amount, DateTime date, string notes)
        {
            ExpenseID = expenseID;
            Name = name;
            Category = category;
            Location = location;
            Amount = amount;
            Date = date;
            Notes = notes;

        }

        public ExpenseModel()
        {

        }

       
    }
}

    
    
