using BankWebbApp.Data;
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
        //public void AddTransaction(Transaction dbTransaction)
        //{
        //    _dbContext.Transactions.Add(dbTransaction);
        //}
        //public void DeleteTransaction(Transaction DeleteTransaction)
        //{
        //    _dbContext.Transactions.Remove(DeleteTransaction);
        //}
        public void Save()
        {
            _dbContext.SaveChanges();
        }

       

      
    }
}
