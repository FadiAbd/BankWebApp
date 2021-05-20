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
}
