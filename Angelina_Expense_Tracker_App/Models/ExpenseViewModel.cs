using Angelina_Expense_Tracker_App_Data_Access.Context;
using Angelina_Expense_Tracker_App_Data_Access.Models;
using Angelina_Expense_Tracker_App_Data_Access.Repositories;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Identity.Client;

namespace Angelina_Expense_Tracker_App.Models
{
    public class ExpenseViewModel
    {
        public ExpenseRepository _repo;

        public List<ExpenseModel> ExpenseList { get; set; }
        public List<ExpenseModel> WeeklyExpenseList { get; set; }

        public ExpenseModel? CurrentExpense { get; set; }

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; } = "";

        public decimal TotalShoppingExpense { get; set; }
        public decimal TotalFoodExpense { get; set; }
        public decimal TotalEducationExpense { get; set; }
        public decimal TotalBillsExpense { get; set; }
        public decimal TotalHealthExpense { get; set; }
        public decimal TotalEntertainmentExpense { get; set; }

        public ExpenseViewModel(Angelina_Expense_Tracker_App_Context context)
        {
            _repo = new ExpenseRepository(context);
            ExpenseList = GetAllExpenses();
            WeeklyExpenseList = GetPastWeekExpenses();
            CurrentExpense = ExpenseList.FirstOrDefault();
        }

        public ExpenseViewModel(Angelina_Expense_Tracker_App_Context context, int expenseID)
        {
            _repo = new ExpenseRepository(context);
            ExpenseList = GetAllExpenses();

            if (expenseID > 0)
            {
                CurrentExpense = GetExpense(expenseID);
            }
            else
            {
                CurrentExpense = new ExpenseModel();
            }
        }

        public void SaveExpense(ExpenseModel expense)
        {
            if (expense.ExpenseID > 0)
            {
                _repo.Update(expense);
            }
            else
            {
                expense.ExpenseID = _repo.Create(expense);
            }

            ExpenseList = GetAllExpenses();
            CurrentExpense = GetExpense(expense.ExpenseID);
        }

        public void DeleteExpense(int expenseID)
        {
            _repo.DeleteExpense(expenseID);
            ExpenseList = GetAllExpenses();
            CurrentExpense = ExpenseList.FirstOrDefault();
        }

        public List<ExpenseModel> GetAllExpenses()
        {
            return _repo.GetAllExpenses();
        }

        public List<ExpenseModel> GetPastWeekExpenses()
        {
            return _repo.GetPastWeekExpenses();
        }

        public ExpenseModel GetExpense(int expenseID)
        {
            return _repo.GetExpenseByID(expenseID);
        }

        

        public decimal GetShoppingTotal()
        {
            decimal shoppingTotal = 0; ;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Shopping")
                {
                    shoppingTotal += ExpenseList[i].Amount;
                }
            }
            return shoppingTotal;
        }

        public decimal GetFoodTotal()
        {
            decimal foodTotal = 0;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Food")
                {
                    foodTotal += ExpenseList[i].Amount;
                }
            }
            return foodTotal;
        }

        public decimal GetEducationTotal()
        {
            decimal educationTotal = 0;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Education")
                {
                    educationTotal += ExpenseList[i].Amount;
                }
            }
            return educationTotal;
        }

        public decimal GetBillsTotal()
        {
            decimal billsTotal = 0;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Bills")
                {
                    billsTotal += ExpenseList[i].Amount;
                }
            }
            return billsTotal;
        }

        public decimal GetHealthTotal()
        {
            decimal healthTotal = 0;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Health")
                {
                    healthTotal += ExpenseList[i].Amount;
                }
                
            }
            return healthTotal;
        }
        
        public decimal GetEntertainmentTotal()
        {
            decimal entertainmentTotal = 0;
            for (int i = 0; i < ExpenseList.Count; i++)
            {
                if (ExpenseList[i].Category == "Entertainment")
                {
                    entertainmentTotal += ExpenseList[i].Amount;
                }
            }
            return entertainmentTotal;
        }
    
    }
}
