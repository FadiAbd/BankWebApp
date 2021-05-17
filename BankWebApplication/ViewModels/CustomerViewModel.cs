using BankWebbApp.Models;
using System;
using System.Collections.Generic;

namespace BankWebbApp.ViewModels
{
    public partial class CustomerIndexViewModel
    {
        public class CustomerViewModel
        {
            public int CustomerId { get; set; }
            public string Gender { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public DateTime? Birthday { get; set; }
            public string NationalId { get; set; }
            public string Telephonecountrycode { get; set; }
            public string Telephonenumber { get; set; }
            public string Emailaddress { get; set; }

            public virtual ICollection<Disposition> Dispositions { get; set; }
            public List<CustomerIndexViewModel> Customers { get; set; } = new List<CustomerIndexViewModel>();
            public List<AccountIndexViewModel> Accounts { get; set; } = new List<AccountIndexViewModel>();
        }

      

    }
}
