using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public partial class UserIndexViewModel
    {
        public string q { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
