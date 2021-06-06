using BankWebbApp.Data;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.TransactionsRowViewModel;

namespace BankWebbApp.Controllers
{
    public class TransactionController : Controller
    {
        
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
          
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;


        }

       
        public IActionResult Index()
        {
            var viewModel = new CustomerTransactionsViewModel();
            viewModel.Transactions = _transactionRepository.GetList(0, 20).OrderByDescending(r => r.Date)


                .Select(r => new TransactionsRowViewModel
                {


                    AccountId = r.AccountId,
                    Date = r.Date,
                    Type = r.Type,
                    Operation = r.Operation,
                    Amount = r.Amount,
                    Balance = r.Balance,
                    Bank = r.Bank,
                    Account = r.Account

                }).ToList();

            return View(viewModel);
        }
        
        public IActionResult GetTransactionsFrom(int skip)
        {
            var viewModel = new TransactionsGetTransactionsFromViewModel();

            viewModel.Transactions = _transactionRepository.GetList(skip, 20).OrderByDescending(r => r.Date)
                .Select(r => new TransactionsRowViewModel
                {

                    TransactionId = r.AccountId,

                    AccountId = r.AccountId,
                    Date = r.Date,
                    Type = r.Type,
                    Operation = r.Operation,
                    Amount = r.Amount,
                    Balance = r.Balance,

                    Bank = r.Bank,
                    Account = r.Account

                }).ToList();

            return View(viewModel);
        }
    }
}