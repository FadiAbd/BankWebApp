using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class AccountTransactionViewModel
    {
        public int TransactionId { get; set; }
        public string Account { get; set; }

        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<CustomerTransactionsViewModel> Customers { get; set; } = new List<CustomerTransactionsViewModel>();

    }
}
