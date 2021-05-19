
using BankWebbApp.Data;
using BankWebbApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankWebbApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts;
        }
        public void AddAccounts(Account dbAccount)
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
