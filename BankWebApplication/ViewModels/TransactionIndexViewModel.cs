using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class TransactionIndexViewModel
    {
       
        
            public string q { get; set; }
            public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
    }
  
}
