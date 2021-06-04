using BankWebbApp.Models;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using static BankWebbApp.ViewModels.CustomerTransactionsViewModel;

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


        [Authorize(Roles = "Admin , Cashier")]
        [HttpGet]
        public IActionResult NewTransfer()
        {
            var viewModel = new NewTransferViewModel();

            return View(viewModel);
        }


        [Authorize(Roles = "Admin , Cashier")]
        [HttpPost]//---------------------------------------------------------------

        public IActionResult NewTransfer(NewTransferViewModel newTransfer)
        {
            var account = _accountRepository.GetAllAccount().FirstOrDefault(r => r.AccountId == newTransfer.AccountId);
            var accountMot = _accountRepository.GetAllAccount().FirstOrDefault(r => r.AccountId == newTransfer.AccountReceiversId);

            if (account == null)
            {
                ModelState.AddModelError("AccountId", "Account not found!");
            }
            else if (accountMot == null)
            {
                ModelState.AddModelError("AccountReceiversId", "Account not found!");

            }
            else if (account.Balance < newTransfer.Amount)
            {
                ModelState.AddModelError("Amount", "Not enough funds in the Account!");
            }
            else if (newTransfer.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Only Positive amount Please!");
            }

            if (ModelState.IsValid)
            {
                var dbTransfer = new Transaction();
                var viewModel = new NewTransferViewModel();
                _transactionRepository.AddTransaction(dbTransfer);
                dbTransfer.AccountId = newTransfer.AccountId;
                dbTransfer.Date = DateTime.Now;
                dbTransfer.Amount = newTransfer.Amount;
                dbTransfer.Type = "Credit";
                dbTransfer.Operation = "Credit in cash";

                var send = _accountRepository.GetAllAccount().First(s => s.AccountId == newTransfer.AccountId);
                send.Balance = send.Balance - newTransfer.Amount;

                var reception = _accountRepository.GetAllAccount().First(r => r.AccountId == newTransfer.AccountReceiversId);
                reception.Balance = reception.Balance + newTransfer.Amount;
                dbTransfer.Balance = send.Balance;

                _transactionRepository.Save();

                //return RedirectToAction("The Transfer is Completed!");
                return RedirectToAction("NewTransfer");
            }
            return View(newTransfer);
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            var viewModel = new NewTransferViewModel();
            var db = _accountRepository.GetAllAccount().First(r => r.AccountId == id);
            if (db == null)
            {
                throw new InvalidDataException("aswraasdf");
            }
            return View(viewModel);
        }





        //----------------------------------------------------------------------------------------------------
        //public ActionResult NewTransfer(TransferViewModel viewModel)
        //{
        //    if (_accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId).Balance < viewModel.Amount)
        //        ModelState.AddModelError("Amount", "Not enough money!");

        //    else if (viewModel.Account == null)
        //    {
        //        ModelState.AddModelError("AccountId", "This account is not valid!");
        //    }



        //    if (ModelState.IsValid)
        //    {
        //        var trans = new Transaction();
        //        _transactionRepository.AddTransaction(trans);
        //        trans.AccountId = viewModel.AccountId;
        //        trans.Date = DateTime.Now;
        //        trans.Type = "Credit";
        //        trans.Operation = "Credit in cash";
        //        trans.Amount = viewModel.Amount;


        //        var dbAcc = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
        //        dbAcc.Balance = dbAcc.Balance - viewModel.Amount;

        //        var dbReciever = _accountRepository.GetAllAccount().First(r => r.AccountId == viewModel.AccountIdReciever);
        //        dbReciever.Balance = dbReciever.Balance + viewModel.Amount;

        //        trans.Balance = dbAcc.Balance;
        //        _transactionRepository.Save();
        //        return RedirectToAction("NewTransfer");

        //    }
        //    return View(viewModel);
        //}

    }
}
