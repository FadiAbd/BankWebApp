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


        [Authorize(Roles = "Admin , Cashier")]
       
        public IActionResult NewDeposit()
        {
            var viewModel = new DepositViewModel();

            return View(viewModel);
        }
        [Authorize(Roles = "Admin , Cashier")]
        
        [HttpPost]
        public ActionResult NewDeposit(DepositViewModel viewModel)
        {
            var account = _accountRepository.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountId);

            if (account == null)
            {
                ModelState.AddModelError("AccountId", "Account not found!");
            }
            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "The amount got to be positive!");
            }


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
