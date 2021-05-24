using BankWebbApp.Data;
using BankWebbApp.Models;
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
using static BankWebbApp.ViewModels.CustomerIndexViewModel;

namespace BankWebbApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customer;
        private readonly IAccountRepository _account;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ApplicationDbContext _dbContext;
        public double totalRowCount { get; private set; }

        public CustomerController( ApplicationDbContext dbContext, ICustomerRepository customer,
            IAccountRepository account,ITransactionRepository transactionRepository)
        {
            _dbContext = dbContext;
            _customer = customer;
            _account = account;
            _transactionRepository = transactionRepository;
        }
        public IActionResult Index(int id,string q,string sortField, string sortOrder,int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();


            _dbContext.Customers
            .Include(a => a.Dispositions)
            .ThenInclude(a => a.Account)
            .FirstOrDefault(a => a.CustomerId == id);
            var query = _dbContext.Customers

               .Where(r => q == null || r.Givenname.Contains(q) || r.City.Contains(q) || r.Streetaddress.Contains(q)
                 || r.NationalId.Contains(q));
            
                int totalCount = query.Count();

            if (string.IsNullOrEmpty(sortField))
                sortField = "Givenname";


            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";


            if (sortField == "Givenname")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.Givenname);
                else
                    query = query.OrderByDescending(y => y.Givenname);
            }

            if (sortField == "Surname")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.Surname);
                else
                    query = query.OrderByDescending(y => y.Surname);
            }

            if (sortField == "NationalId")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.NationalId);
                else
                    query = query.OrderByDescending(y => y.NationalId);
            }

            if (sortField == "StreetAdress")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.Streetaddress);
                else
                    query = query.OrderByDescending(y => y.Streetaddress);
            }

            if (sortField == "City")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.City);
                else
                    query = query.OrderByDescending(y => y.City);
            }

            int pageSize = 50;

            var pageCount = (double)totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            int howManyRecordsToSkip = (page - 1) * pageSize;//1 => 0
            query = query.Skip(howManyRecordsToSkip).Take(pageSize);


            viewModel.Customers = query

            .Select(f => new CustomerIndexViewModel.CustomerViewModel
            {
                CustomerId = f.CustomerId,
                Givenname = f.Givenname,
                Surname = f.Surname,
                NationalId = f.NationalId,
                Streetaddress = f.Streetaddress,
                City = f.City,
                
                
                
                 }).ToList();
            viewModel.q = q;
            viewModel.sortOrder = sortOrder;
            viewModel.sortField = sortField;
            viewModel.Page = page;
            viewModel.OppositeSortOrder = sortOrder == "asc" ? "desc" : "asc";


            return View(viewModel);
        }
      //[Authorize(Roles = "Cashier")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult CustomerDetails(int Id)
        {
            var viewModel = new CustomerDetailsViewModel();
            var query = (from c in _dbContext.Customers
                         join d in _dbContext.Dispositions on c.CustomerId equals d.CustomerId
                         join a in _dbContext.Accounts on d.AccountId equals a.AccountId
                         where c.CustomerId == Id
                         select new
                         {
                             c.CustomerId,
                             c.Givenname,
                             c.Surname,
                             c.Streetaddress,
                             c.City,
                             c.Country,
                             a.AccountId,
                             a.Balance
                         });

            foreach (var q in query)
            {
                viewModel.CustomerId = q.CustomerId;
                viewModel.Givenname = q.Givenname;
                viewModel.Surename = q.Surname;
                viewModel.StreetAdress = q.Streetaddress;
                viewModel.City = q.City;
                viewModel.Country = q.Country;
            }
            viewModel.Accounts = query.Select(r => new CustomerDetailsViewModel.AccountDetails
            {
                AccountId = r.AccountId,
                
                Balance = r.Balance
            }).ToList();

            return View(viewModel);
        }

        //[Authorize(Roles = "Cashier")]
        //[Authorize(Roles = "Admin")]
        public IActionResult CustomerTransactions(int id)
        {
            var viewModel = new CustomerTransactionsViewModel();

            var dbTransaction = _dbContext.Transactions.Where(r => r.AccountId == id);


            viewModel.Transactions = dbTransaction.Select(t => new CustomerTransactionsViewModel.Transaction
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                Date = t.Date,
                Type = t.Type,
                Operation = t.Operation,
                Amount = t.Amount,
                Balance = t.Balance,
                Bank = t.Bank,
                Account = t.Account
            }).ToList();


            return View(viewModel);
        }

       



            //public IActionResult _SelectCustomer(int selectedId)
            //{
            //    var viewModel = new SelectCustomerViewModel();
            //    var selectedCustomer = _dbContext.Customers.First(r => r.CustomerId == selectedId );

            //    viewModel.Gender = selectedCustomer.Gender;
            //    viewModel.Givenname = selectedCustomer.Givenname;
            //    viewModel.Surname = selectedCustomer.Surname;






            //    return View(viewModel);
            //}
            //[Authorize(Roles = "Admin")]
            //[Authorize(Roles = "Cashier")]---------------------------------------

            //public IActionResult CustomerPage(int id)
            //{
            //    var viewModel = new CustomerPageViewModel();
            //    if(_customer.GetAllCustomer().Include(x => x.Dispositions)
            //        .FirstOrDefault(r => r.CustomerId == id) == null)
            //    {
            //        //viewModel.DoNotExist = true;
            //        return View(viewModel);
            //    }
            //    var p = _customer.GetAllCustomer().Include(x => x.Dispositions)
            //        .First(r => r.CustomerId == id);

            //    viewModel.CustomerId = p.CustomerId;
            //    viewModel.Gender = p.Gender;
            //    viewModel.Givenname = p.Givenname;
            //    viewModel.Surname = p.Surname;
            //    viewModel.NationalId = p.NationalId;
            //    viewModel.Streetaddress = p.Streetaddress;
            //    viewModel.Birthday = p.Birthday;
            //    viewModel.City = p.City;
            //    viewModel.Country = p.Country;
            //    viewModel.CountryCode = p.CountryCode;
            //    viewModel.Emailaddress = p.Emailaddress;
            //    viewModel.Telephonecountrycode = p.Telephonecountrycode;
            //    viewModel.Telephonenumber = p.Telephonenumber;
            //    viewModel.Zipcode = p.Zipcode;

            //    var dispAcc = p.Dispositions.ToList();

            //    foreach(var d in dispAcc)
            //    {
            //        var account = new CustomerAccountViewModel();
            //        var dbacc = _account.GetAllAccount().First(n => n.AccountId.Equals(d.AccountId));
            //        account.AccountId = dbacc.AccountId;
            //        account.Balance = dbacc.Balance;
            //        account.Created = dbacc.Created;
            //        account.Frequency = dbacc.Frequency;

            //        viewModel.Account.Add(account);
            //    }
            //    viewModel.SumOffCustomerAccounts = viewModel.Account.Sum(x => x.Balance);


            //    return View(viewModel);
            //}------------------------------------------------------

























        }


}
