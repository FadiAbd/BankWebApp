using BankWebbApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Controllers
{
    public class TransferController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;




        public TransferController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
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
        public ActionResult NewWithdrawal(TransferViewModel viewModel)
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
