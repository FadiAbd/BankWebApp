using BankWebbApp.Data;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        

        public TransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

       
        public IActionResult Index(string q)
        {
            var viewModel = new TransactionIndexViewModel();
            viewModel.Transactions = _dbContext.Transactions
                .Where(r => q == null || r.Account.Contains(q) || r.Bank.Contains(q))
                .Select(transaction => new TransactionViewModel
                {
                  AccountId = transaction.AccountId,
                  Date = transaction.Date,
                  Amount = transaction.Amount,
                  Balance = transaction.Balance,
                  Bank =transaction.Bank

                }).ToList();

                return View(viewModel);
        }
    }
}
