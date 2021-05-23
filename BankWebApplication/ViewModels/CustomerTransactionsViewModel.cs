﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BankWebbApp.ViewModels
{
    public class CustomerTransactionsViewModel
    {

        public List<Transaction> Transactions { get; set; }

        public class Transaction
        {
            public int TransactionId { get; set; }
            public int AccountId { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public decimal Amount { get; set; }
            public decimal Balance { get; set; }
            public string Bank { get; set; }
            public string Account { get; set; }
        }

    }
   
}
