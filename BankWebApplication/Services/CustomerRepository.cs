using BankWebbApp.Models;
using System.Collections.Generic;

namespace BankWebbApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>();
        }

       
    }
}
