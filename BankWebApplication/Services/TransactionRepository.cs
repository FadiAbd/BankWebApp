using BankWebbApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BankWebbApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private static Random rand = new Random();
        private static List<Transaction> Transactions;
        public List<Transaction> GetList(int skip, int antal)
        {
            return Transactions.Skip(skip).Take(antal).ToList();
        }

        public void AddTransaction(Transaction dbTransaction)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTransaction(Transaction DeleteTransaction)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Transaction> GetAllTransaction()
        {
            return (IQueryable<Transaction>)_dbContext.Transactions;
        }
       
        public void Save()
        {
            _dbContext.SaveChanges();
        }

       

      
    }
}
