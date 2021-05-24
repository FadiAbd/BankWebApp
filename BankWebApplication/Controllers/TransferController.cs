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
    public class TransferController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;



        public TransferController( IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
       



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewTransfer()
        {
            var viewModel = new TransferViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NewTransfer(TransferViewModel viewModel)
        {
            if (_accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId).Balance < viewModel.Amount)
                ModelState.AddModelError("Amount", "Not enough money!");

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
                dbAcc.Balance = dbAcc.Balance - viewModel.Amount;

                var dbReciever = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountIdReciever);
                dbReciever.Balance = dbReciever.Balance + viewModel.Amount;

                trans.Balance = dbAcc.Balance;
                _transactionRepository.Save();
                return RedirectToAction("NewTransfer");

            }
            return View(viewModel);
        }

    }
}
