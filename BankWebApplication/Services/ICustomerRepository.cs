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
        IQueryable<Customer> GetAllCustomer();
        public void AddCustomer(Customer dbCustomer);
        public void DeleteCustomer(Customer DeleteCustomer);
        public void Save();

       
    }

    public class CustomerRepository : ICustomerRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetAllCustomer()
        {
            return _dbContext.Customers;
        }
        public void AddCustomer(Customer dbCustomer)
        {
            _dbContext.Customers.Add(dbCustomer);
        }
        public void DeleteCustomer(Customer DeleteCustomer)
        {
            _dbContext.Customers.Remove(DeleteCustomer);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
