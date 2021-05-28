using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class UserDeleteViewModel
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string LoginName { get; set; }
        [Required]
        [MaxLength(20)]
        public byte[] PasswordHash { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
