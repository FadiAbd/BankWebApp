using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext dbContext , UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
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
