﻿using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class TransactionIndexViewModel
    {

       
        
            public string q { get; set; }

        public List<Transaction> Transactions { get; set; }
        public int TransactionId { get; set; }

        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }

        public virtual Account AccountNavigation { get; set; }

    
        
       

       
    }
   
   


}
  