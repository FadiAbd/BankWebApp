using BankWebbApp.Data;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BankWebbApp.Repository
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllTransaction();
        List<Transaction> GetList(int skip, int antal);
        public void AddTransaction(Transaction dbTransaction);
        public void DeleteTransaction(Transaction DeleteTransaction);
        public void Save();


        
    }
}
