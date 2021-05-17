using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.CustomerIndexViewModel;

namespace BankWebbApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public double totalRowCount { get; private set; }

        public CustomerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public IActionResult Index(string q,string sortField, string sortOrder,int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();

            var query = _dbContext.Customers

                .Where(r => q == null || r.Givenname.Contains(q) || r.City.Contains(q) || r.Streetaddress.Contains(q)
                 || r.NationalId.Contains(q));
            
                int totalCount = query.Count();

            if (string.IsNullOrEmpty(sortField))
                sortField = "Givenname";


            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";

            if (sortField == "Title")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(y => y.Givenname);
                else
                    query = query.OrderByDescending(y => y.Givenname);
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
            int pageSize = 20;

            var pageCount = (double)totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            int howManyRecordsToSkip = (page - 1) * pageSize;//1 => 0
            query = query.Skip(howManyRecordsToSkip).Take(pageSize);


            viewModel.Customers = query

            .Select(f => new CustomerViewModel
            {
                CustomerId = f.CustomerId,
                Givenname = f.Givenname,
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
        public IActionResult _SelectCustomer(int selectedId)
        {
            var viewModel = new SelectCustomerViewModel();
            var selectedCustomer = _dbContext.Customers.First(r => r.CustomerId == selectedId );

            viewModel.Gender = selectedCustomer.Gender;
            viewModel.Givenname = selectedCustomer.Givenname;
            viewModel.Surname = selectedCustomer.Surname;





            return View(viewModel);
        }

        public IActionResult CustomerDetails(string q)
        {
            

            var viewModel = new CustomerIndexViewModel();

            viewModel.Customers = _dbContext.Customers.Where(r => q == null || r.Givenname.Contains(q)
           
                 || r.NationalId.Contains(q))
                .Select(dbC => new CustomerViewModel
                {
                    CustomerId = dbC.CustomerId,
                    Gender = dbC.Gender,
                    Givenname = dbC.Givenname,
                    Surname = dbC.Surname,
                    NationalId = dbC.NationalId,
                    Streetaddress = dbC.Streetaddress,
                    Birthday = dbC.Birthday,
                    City = dbC.City,
                    Country = dbC.Country,
                    CountryCode = dbC.CountryCode,
                    Emailaddress = dbC.Emailaddress,
                    Telephonecountrycode = dbC.Telephonecountrycode,
                    Telephonenumber = dbC.Telephonenumber,
                    Zipcode = dbC.Zipcode,
                    
                    
                    



                }).ToList();


            return View(viewModel);
        }



        //public IActionResult Search(string q)
        //{
        //    var viewModel = new CustomerSearchViewModel();

        //    viewModel.Customers = _dbContext.Customers.Include(r => r.Supplier)
        //        .Where(r => q == null || r.Namn.Contains(q) || r.Supplier.Name.Contains(q))
        //        .Select(dbVacc => new VaccinViewModel
        //        {
        //            Id = dbVacc.Id,
        //            Supplier = dbVacc.Supplier.Name,
        //            Name = dbVacc.Namn
        //        }).ToList();


        //    return View(viewModel);
        //}
    }
}
