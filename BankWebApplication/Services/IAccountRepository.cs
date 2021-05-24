using BankWebbApp.Data;

using BankWebbApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Repository
{
     public interface IAccountRepository
    {
      
        IQueryable<Account> GetAllAccount();
        public void AddAccount(Account dbAccount);
        public void DeleteCustomer(Account DeleteAccount);
        public void Save();



    }
    public class AccountRepository : IAccountRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Account> GetAllAccount()
        {
            return _dbContext.Accounts;
        }
        public void AddAccount(Account dbAccount)
        {
            _dbContext.Accounts.Add(dbAccount);
        }
        public void DeleteAccount(Account DeleteAccount)
        {
            _dbContext.Accounts.Remove(DeleteAccount);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void DeleteCustomer(Account DeleteAccount)
        {
            _dbContext.Accounts.Remove(DeleteAccount);

        }

    }
}
