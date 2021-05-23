using BankWebbApp.Models;
using System.Collections.Generic;

namespace BankWebbApp.ViewModels
{
    public partial class UserIndexViewModel
    {
        public class UserViewModel
        {
            public int UserId { get; set; }

            public string LoginName { get; set; }

            public byte[] PasswordHash { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
            public List<User> Users { get; set; }
        }
    }
}
