using BankWebbApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BankWebbApp.ViewModels.AccountIndexViewModel;

namespace BankWebbApp.ViewModels
{
    public partial class CustomerIndexViewModel
    {
        public string q { get; set; }
        public string sortOrder { get; set; }
        public string sortField { get; set; }
        public string OppositeSortOrder { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public List<Account> Accounts { get; set; } = new List<Account>();



    }
}
