﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.CustomerIndexViewModel;

namespace BankWebbApp.ViewModels
{
    public class CustomerPageViewModel
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
        //public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public List<CustomerAccountViewModel> Account { get; set; } = new List<CustomerAccountViewModel>();
        public decimal SumOffCustomerAccounts { get; set; }
        public bool DoNotExist { get; set; }

    }
}
