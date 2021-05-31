using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.UserIndexViewModel;

namespace BankWebbApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
       private readonly UserManager<IdentityUser> _userManager;
     

        public UserController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
           
        }
        
        public IActionResult Index(string q)
        {
            var viewModel = new UserIndexViewModel();
            viewModel.Users = _dbContext.Users
            .Where(r => q == null || r.LoginName.Contains(q) || r.FirstName.Contains(q))
                .Select(user => new UserViewModel
                {
                    UserId = user.UserId,
                    LoginName = user.LoginName,
                    PasswordHash = user.PasswordHash,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }).ToList();

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        
        public IActionResult New()
        {
            var viewModel = new UserNewViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
       
        public IActionResult Edit()
        {
            var viewModel = new UserEditViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult Delete()
        {
            var viewModel = new UserDeleteViewModel();
            return View(viewModel);
        }





    }
}
