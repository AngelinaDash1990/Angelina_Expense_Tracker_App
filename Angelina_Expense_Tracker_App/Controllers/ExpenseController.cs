using Angelina_Expense_Tracker_App.Models;
using Angelina_Expense_Tracker_App_Data_Access.Models;
using Angelina_Expense_Tracker_App_Data_Access.Context;
using Angelina_Expense_Tracker_App_Data_Access.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Angelina_Expense_Tracker_App.Controllers
{

    public class ExpenseController : Controller
    {
        public Angelina_Expense_Tracker_App_Context _context;

        public ExpenseController(Angelina_Expense_Tracker_App_Context context)
        {
            _context = context;
        }


        
        public IActionResult Main()
        {
            ExpenseViewModel model = new ExpenseViewModel(_context);

            model.TotalShoppingExpense = model.GetShoppingTotal();
            model.TotalFoodExpense = model.GetFoodTotal();
            model.TotalEducationExpense = model.GetEducationTotal();
            model.TotalBillsExpense = model.GetBillsTotal();
            model.TotalHealthExpense = model.GetHealthTotal();
            model.TotalEntertainmentExpense = model.GetEntertainmentTotal();
            return View(model);
        }

        [HttpGet]
        public IActionResult NewExpense()
        {
            ExpenseViewModel model = new ExpenseViewModel(_context)
            {
                CurrentExpense = new ExpenseModel() // Create a new, empty Expense object
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult NewExpense(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                _context.Expense.Add(expense);
                _context.SaveChanges();

                return RedirectToAction(nameof(Main));
            }

            // If model is invalid, return to view with current expense
            ExpenseViewModel model = new ExpenseViewModel(_context)
            {
                CurrentExpense = expense
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateExpense(int expenseID)
        {
            var expense = _context.Expense.Find(expenseID);
            if (expense == null)
            {
                return NotFound();
            }
            ExpenseViewModel model = new ExpenseViewModel(_context)
            {
                CurrentExpense = expense
            };
            return View(model);
        }

        
        [HttpPost]
        public IActionResult UpdateExpense(int expenseID, string name, string category, string location, decimal amount, DateTime date, string notes)
        {
            ExpenseViewModel model = new ExpenseViewModel(_context);

            ExpenseModel expense = new(expenseID, name, category, location, amount, date, notes);

            model.SaveExpense(expense);
            model.IsActionSuccess = true;
            model.ActionMessage = "Model has been updated successfully!";

            return RedirectToAction(nameof(Main));
        }

        public IActionResult DeleteExpense(int id)
        {
            ExpenseViewModel model = new ExpenseViewModel(_context);

            if (id > 0)
            {
                model.DeleteExpense(id);
            }

            model.IsActionSuccess = true;
            model.ActionMessage = "Expense has been deleted successfully";
            return View("Main", model);
        }
    }
}
