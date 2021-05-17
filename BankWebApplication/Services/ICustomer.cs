using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Services
{
    public interface  ICustomer
    {
        IEnumerable<Customer> GetCustomers { get; }
        Customer GetCustomer(int CustomerId);
    }
}
