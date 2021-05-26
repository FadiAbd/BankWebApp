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
//using static BankWebbApp.ViewModels.CustomerTransactionsViewModel;

namespace BankWebbApp.Controllers
{
    public class DepositController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;




        public DepositController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {

            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;

        }

        public IActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = "Cashier")]
        [HttpGet]
       
        public IActionResult NewDeposit()
        {
            var viewModel = new DepositViewModel();

            return View(viewModel);
        }
        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public ActionResult NewDeposit(DepositViewModel viewModel)
        {
            

            if (ModelState.IsValid)
            {
                var trans = new Transaction();
                _transactionRepository.AddTransaction(trans);
                trans.AccountId = viewModel.AccountId;
                trans.Date = DateTime.Now;
                trans.Type = "Credit";
                trans.Operation = "Credit in cash";
                trans.Amount = viewModel.Amount;


                var dbAcc = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance - viewModel.Amount;

                trans.Balance = balance;
                dbAcc.Balance = balance;

                _transactionRepository.Save();
                return RedirectToAction("NewDeposit");

            }
            return View(viewModel);
        }


    }
}
