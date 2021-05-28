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
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IDispositionRepository _dispositionRepository;



        //private readonly ApplicationDbContext _dbContext;
        public double totalRowCount { get; private set; }

        public CustomerController( /*ApplicationDbContext dbContext,*/ ICustomerRepository customerRepository,
            IAccountRepository accountRepository,ITransactionRepository transactionRepository,
            IDispositionRepository dispositionRepository)
        {
            //_dbContext = dbContext;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _dispositionRepository = dispositionRepository;
        }
       // [Authorize(Roles = "Cashier")]
        public IActionResult Index(int id,string q,string sortField, string sortOrder,int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();


            //_customerRepository.Customers
            //.Include(a => a.Dispositions)
            //.ThenInclude(a => a.Account)
            //.FirstOrDefault(a => a.CustomerId == id);
            var query = _customerRepository.GetAllCustomers()

               .Where(r => q == null || r.Givenname.Contains(q) || r.City.Contains(q) || r.Streetaddress.Contains(q)
                 || r.NationalId.Contains(q));
            
                int totalCount = query.Count();

            if (string.IsNullOrEmpty(sortField))
                sortField = "CustomerId";


            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";

            if (sortField == "CustomerId")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.CustomerId);
                else
                    query = query.OrderByDescending(y => y.CustomerId);
            }

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

            if (sortField == "Country")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.Country);
                else
                    query = query.OrderByDescending(y => y.Country);
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
                NationalId = f.NationalId,
                Givenname = f.Givenname,
                Surname = f.Surname,
                
                Streetaddress = f.Streetaddress,
                City = f.City,
                Country = f.Country,



            }).ToList();
            viewModel.q = q;
            viewModel.sortOrder = sortOrder;
            viewModel.sortField = sortField;
            viewModel.Page = page;
            viewModel.OppositeSortOrder = sortOrder == "asc" ? "desc" : "asc";


            return View(viewModel);
        }
      //[Authorize(Roles = "Cashier")]
   
        public IActionResult CustomerDetails(int Id)
        {
            var viewModel = new CustomerDetailsViewModel();
            var query = (from c in _customerRepository.GetAllCustomers()
                         join d in _dispositionRepository.GetAllDispositions() on c.CustomerId equals d.CustomerId
                         join a in _accountRepository.GetAllAccount() on d.AccountId equals a.AccountId
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

        
        public IActionResult CustomerTransactions(int id)
        {
            var viewModel = new CustomerTransactionsViewModel();

            var dbTransaction = _transactionRepository.GetAllTransactions().Where(r => r.AccountId == id);


            viewModel.Transactions = dbTransaction.Select(t => new TransactionsRowViewModel 
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

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Cashier")]
        public IActionResult New()
        {
            var viewModel = new CustomerNewViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Cashier")]
         public IActionResult Edit()
        {
            var viewModel = new CustomerEditViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Cashier")]
         public IActionResult Delete()
        {
            var viewModel = new CustomerDeleteViewModel();
            return View(viewModel);
        }



    }


}
