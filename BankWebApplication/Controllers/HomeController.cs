using BankWebbApp.Data;
using BankWebbApp.Models;

using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
       

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();

            viewModel.AllCustomers = _dbContext.Customers.Count();
            viewModel.AllAccounts = _dbContext.Accounts.Count();
            viewModel.TotalBalanceAllAccounts = (int)_dbContext.Accounts.Sum(r => r.Balance);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
