using BankWebbApp.Models;
using System;
using System.Collections.Generic;

namespace BankWebbApp.ViewModels
{
   
    
        public class AccountViewModel
        {


            public int AccountId { get; set; }
            public string Frequency { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }



            public virtual ICollection<Disposition> Dispositions { get; set; }
            public virtual ICollection<Loan> Loans { get; set; }
            public virtual ICollection<PermenentOrder> PermenentOrders { get; set; }
            public virtual ICollection<Transaction> Transactions { get; set; }

        }
      

    
}
