using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public partial class AccountIndexViewModel
    {       public int q { get; set; }
            public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
      

    }
}
