using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        
       
        private readonly IAccountRepository _accountRepository;
       




        public double totalRowCount { get; private set; }

        public AccountController(IAccountRepository accountRepository)
        {

           
            _accountRepository = accountRepository;
          
        }
        public IActionResult Index(string q)
        {
            var viewModel = new AccountIndexViewModel();
            viewModel.Accounts = _accountRepository.GetAllAccount()

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

        [Authorize(Roles = "Admin , Cashier")]
        
        public IActionResult New()
        {
            var viewModel = new AccountNewViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin , Cashier")]
        public IActionResult Edit()
        {
            var viewModel = new AccountEditViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin , Cashier")]
        public IActionResult Delete()
        {
            var viewModel = new AccountDeleteViewModel();
            return View(viewModel);
        }

    }
}
