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
        public IActionResult Edit(int id)
        {
            var viewModel = new UserEditViewModel();
            var dbUser = _dbContext.Users.First(r => r.UserId == id );

            viewModel.UserId = dbUser.UserId;
            viewModel.LoginName = dbUser.LoginName;
            viewModel.PasswordHash = dbUser.PasswordHash;
            viewModel.FirstName = dbUser.FirstName;
            viewModel.LastName = dbUser.LastName;

            return View(viewModel);
        }






        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            var viewModel = new UserNewViewModel();

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult New(UserNewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbUser = new User();
               // _dbContext.Users.Add(dbUser);
                dbUser.UserId = viewModel.UserId;
                dbUser.FirstName = viewModel.FirstName;
                dbUser.LastName = viewModel.LastName;
                dbUser.LoginName = viewModel.LoginName;
                dbUser.PasswordHash = viewModel.PasswordHash;
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
           

            return View(viewModel);
        }



    }
}
