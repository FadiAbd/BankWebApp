using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public int AllCustomers  { get; set; }
        public int AllAccounts { get; set; }
        public int TotalBalanceAllAccounts { get; set; }
        public List<HomeIndexViewModel> Balance = new List<HomeIndexViewModel>();
    }
}
