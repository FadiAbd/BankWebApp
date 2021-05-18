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
  

        List<Account> GetAllAccounts();
    }
}
