using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Angelina_Expense_Tracker_App_Data_Access.Models;
using Angelina_Expense_Tracker_App_Data_Access.Context;

namespace Angelina_Expense_Tracker_App_Data_Access.Repositories
{
    public class ExpenseRepository
    {
        public Angelina_Expense_Tracker_App_Context? _dbContext;

        public ExpenseRepository(Angelina_Expense_Tracker_App_Context dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(ExpenseModel expense)
        {

            _dbContext.Add(expense);
            _dbContext.SaveChanges();

            return expense.ExpenseID;
        }

        public int Update(ExpenseModel expense)
        {
            ExpenseModel? existingExpense = _dbContext.Expense.Find(expense.ExpenseID);

            if (existingExpense == null)
            {
                // Handle the case where the expense doesn't exist
                throw new ArgumentException($"Expense with ID {expense.ExpenseID} not found");
            }

            existingExpense.Name = expense.Name;
            existingExpense.Category= expense.Category;
            existingExpense.Amount = expense.Amount;
            existingExpense.Date = expense.Date;
            existingExpense.Notes = expense.Notes;

            _dbContext.SaveChanges();

            return existingExpense.ExpenseID;
        }

        public bool DeleteExpense(int expenseID)
        {
            ExpenseModel expense = _dbContext.Expense.Find(expenseID);
            _dbContext.Remove(expense);
            _dbContext.SaveChanges();

            return true;
        }

        public List<ExpenseModel> GetAllExpenses()
        {
            List<ExpenseModel> expenseList = _dbContext.Expense.ToList();

            return expenseList;
        }

        public List<ExpenseModel> GetPastWeekExpenses()
        {
            List<ExpenseModel> weeklyList = _dbContext.Expense.Where(e => e.Date >= DateTime
            .Now.AddDays(-7)).ToList();

            return weeklyList;
        }

        public ExpenseModel GetExpenseByID(int expenseId)
        {
            ExpenseModel expense = _dbContext.Expense.Find(expenseId)!;
            return expense;
        }
    }
}
