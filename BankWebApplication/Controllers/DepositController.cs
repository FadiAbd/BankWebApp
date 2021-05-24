using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.CustomerTransactionsViewModel;

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

        public IActionResult New()
        {
            var viewModel = new DepositViewModel();

            return View(viewModel);
        }

        public ActionResult New(DepositViewModel viewModel)
        {
            

            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transactionRepository.AddTransaction(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = "Credit";
                dbTrans.Operation = "Credit in cash";
                dbTrans.Amount = viewModel.Amount;


                var dbAcc = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance - viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;

                _transactionRepository.Save();
                return RedirectToAction("New");

            }
            return View(viewModel);
        }


    }
}
