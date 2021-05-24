using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Repository;
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
        private readonly IAccountRepository _account;
        private readonly ITransactionRepository _transaction;

        private readonly ApplicationDbContext _dbContext;
     

        public AccountController( ApplicationDbContext dbContext, IAccountRepository account,ITransactionRepository transaction)
        {
            _dbContext = dbContext;
            _account = account;
            _transaction = transaction;
            
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
    //    public IActionResult AccountPage(int id)
    //    {
    //        var viewModel = new AccountPageViewModel();
    //        if (_account.GetAllAccounts().Include(x => x.Transactions)
    //            .FirstOrDefault(r => r.CustomerId == id) == null)
    //        {
    //            viewModel.DoNotExist = true;
    //            return View(viewModel);
    //        }
    //        var p = _account.GetAllAccounts().Include(x => x.Transactions)
    //            .First(r => r.AccountId == id);

    //        viewModel.AccountId = p.AccountId;
    //        viewModel.Balance = p.Balance;
    //        //viewModel. = p.;
    //        //viewModel. = p.;
           

    //        var accTr = p.Transactions.ToList();

    //        foreach (var d in accTr)
    //        {
    //            var transaction  = new AccountTransactionViewModel();
    //            var dbacc = _transaction.GetAllTransactions().First(n => n.Equals(d.TransactionId));
    //            transaction.AccountId = dbacc.;
    //            transaction.Account = dbacc.;
    //            transaction.Balance = dbacc.Created;
    //            transaction.Frequency = dbacc.Frequency;

    //            viewModel.AccountId.Add(account);
    //        }
    //        //viewModel.SumOffCustomerAccounts = viewModel.Account.Sum(x => x.Balance);


    //        return View(viewModel);
    //    }
    //        return View();
    //}
}
}
