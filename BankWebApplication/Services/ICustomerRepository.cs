using BankWebbApp.Data;
using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Repository
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomers();
        public void AddCustomer(Customer dbCustomer);
        public void DeleteCustomer(Customer DeleteCustomer);
        public void Save();

       
    }
}
