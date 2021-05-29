using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        //private readonly SignInManager<IdendityUser> _signInManager;
        




       

        public HomeController(ILogger<HomeController> logger,ICustomerRepository customerRepository, IAccountRepository accountRepository
           /*,SignInManager<IdendityUser>signInManager*/)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            //_signInManager = signInManager;
           
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any) ]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.Customers = _customerRepository.GetAllCustomers().Count();
            viewModel.Accounts = _accountRepository.GetAllAccount().Count();
            viewModel.Amount = _accountRepository.GetAllAccount().Sum(r => r.Balance);
           
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
