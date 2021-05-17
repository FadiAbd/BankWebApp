using BankWebbApp.Models;
using BankWebbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Services
{
    public interface IAccount
    {

        IEnumerable<Account> GetAccounts { get; }
        Account GetAccount(int AccountId);
        
     
        
       
    }
    
    
}
