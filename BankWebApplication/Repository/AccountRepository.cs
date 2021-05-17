using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Repository
{
    public class AccountRepository : IAccount
    {
        private ApplicationDbContext _dbContext;
        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Account> GetAccounts => _dbContext.Accounts;

        public Account GetAccount(int AccountId)
        {
            Account dbEntity = _dbContext.Accounts.Find(AccountId);
            return dbEntity;
        }
    }
}
