using BankWebbApp.Data;
using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Services
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllTransactions();
        List<Transaction> GetList(int skip, int antal);
        public void AddTransaction(Transaction transaction);
        public void Save();
    }

    public class TransactionRepository : ITransactionRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return _dbContext.Transactions;
        }
        public List<Transaction> GetList(int skip, int antal)
        {
            return _dbContext.Transactions.Skip(skip).Take(antal).ToList();
        }
        public void AddTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }



    }
}
