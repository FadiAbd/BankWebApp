using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class UserNewViewModel
    {
        public int UserId { get; set; }

        public string LoginName { get; set; }

        public byte[] PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
