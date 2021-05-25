
using BankWebbApp.Models;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using static BankWebbApp.ViewModels.CustomerTransactionsViewModel;

namespace BankWebbApp.Controllers
{
    public class WithdrawalController : Controller
    {

        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        


        public WithdrawalController( IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;

        }

        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult NewWithdrawal()
        {
            var viewModel = new WithdrawalViewModel();

            return View();
        }
        [HttpPost]
        public ActionResult NewWithdrawal(WithdrawalViewModel viewModel)
        {
            if (_accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId).Balance < viewModel.Amount)
                ModelState.AddModelError("Amount", "Not enough money!");

            if (ModelState.IsValid)
            {
                var trans = new Transaction();
                _transactionRepository.AddTransaction(trans);
                trans.AccountId = viewModel.AccountId;
                trans.Date = DateTime.Now;
                trans.Type = "Debit";
                trans.Operation = "Withdrawal in cash";
                trans.Amount = decimal.Negate(viewModel.Amount);


                var dbAcc = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance - viewModel.Amount;

                _transactionRepository.Save();
                return RedirectToAction("NewWithdrawal");

            }
            return View(viewModel);
        }
       
    }
}
