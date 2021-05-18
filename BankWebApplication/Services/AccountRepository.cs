
using BankWebbApp.Models;
using System.Collections.Generic;

namespace BankWebbApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
       public List<Account> GetAllAccounts()
        {
            return new List<Account>();
        }

        
    }
}
