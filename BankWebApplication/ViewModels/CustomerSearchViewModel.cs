using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class CustomerSearchViewModel
    {
        public List<CustomerIndexViewModel> Customers { get; set; } = new List<CustomerIndexViewModel>();
    }
}
