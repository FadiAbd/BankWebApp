using System.Collections.Generic;
using System.Transactions;

namespace BankWebbApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<Transaction> GetAllTransactions()
        {
            return new List<Transaction>();
        }
    }
}
