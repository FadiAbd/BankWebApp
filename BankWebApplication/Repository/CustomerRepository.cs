using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Repository
{
    public class CustomerRepository:ICustomer
    {
        private ApplicationDbContext _dbContext;
        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Customer> GetCustomers => _dbContext.Customers;

        public Customer GetCustomer(int CustomerId)
        {
            Customer dbEntity = _dbContext.Customers.Find(CustomerId);
            return dbEntity;
        }
    }
}
