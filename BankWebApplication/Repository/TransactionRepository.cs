using BankWebbApp.Data;
using BankWebbApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BankWebbApp.Repository
{
    //public class TransactionRepository:ITransaction
    //{
    //    //private ApplicationDbContext _dbContext;
    //    //public TransactionRepository(ApplicationDbContext dbContext)
    //    //{
    //    //    _dbContext = dbContext;
    //    //}
    //    //public IEnumerable<Transaction> Transactions => _dbContext.Transactions;

    //    //public IEnumerable<Models.Transaction> GetTransactions { get; }

    //    //public Transaction GetTransaction(int TransactionId)
    //    //{
    //    //    Transaction dbEntity = _dbContext.Transactions.Find(TransactionId);
    //    //    return dbEntity;
    //    //}

       
    //}
}
