using BankWebbApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.Migrate();
            SeedCustomers(dbContext);
            SeedAccounts(dbContext);
            SeedUsers(userManager, dbContext);
            SeedTransactions(dbContext);

        }

        private static void SeedTransactions(ApplicationDbContext dbContext)
        {
            var transaction = dbContext.Transactions.FirstOrDefault(r => r.TransactionId == 0);
            if (transaction == null)
                dbContext.Transactions.Add(new Transaction { TransactionId = 0 });
        }

        private static void SeedAccounts( ApplicationDbContext dbContext)
        {
            var account = dbContext.Accounts.FirstOrDefault(r => r.AccountId == 0 );
            if (account == null)
                dbContext.Accounts.Add(new Account { AccountId = 0 });
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager,ApplicationDbContext dbContext)
        {
            AddUserIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists(userManager, "stefan.holmberg@nackademin.se", "Hejsan123#", new string[] { "Cashier" });
        }

       

            private static void AddUserIfNotExists(UserManager<IdentityUser> userManager,
              string userName, string password, string[] roles)
            {
                if (userManager.FindByEmailAsync(userName).Result != null) return;

                var user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, password).Result;
                var r = userManager.AddToRolesAsync(user, roles).Result;
            }

            private static void SeedCustomers(ApplicationDbContext dbContext)
        {
            var customer = dbContext.Customers.FirstOrDefault(r => r.CustomerId == 0);
            if (customer == null)
                dbContext.Customers.Add(new Customer { CustomerId = 0 });
        }

       

      
    }
}
