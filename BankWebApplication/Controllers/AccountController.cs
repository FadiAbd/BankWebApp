using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.AccountIndexViewModel;

namespace BankWebbApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _Account;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext ,IAccount _IAccount)
        {
            _dbContext = dbContext;
            _Account = _IAccount;
        }
        public IActionResult Index(string q)
        {
            var viewModel = new AccountIndexViewModel();
            viewModel.Accounts = _dbContext.Accounts

               .Select(dbAcc => new AccountViewModel
               {
                   AccountId = dbAcc.AccountId,
                   Balance = dbAcc.Balance,
                   Frequency = dbAcc.Frequency,
                   Created = dbAcc.Created,
                   Transactions = dbAcc.Transactions,
                   
                   
                    
                  


               }).ToList();
               
            return View(viewModel);
        }
    }
}
