using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class UserNewViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please write a name!")]
        [MaxLength(30, ErrorMessage = "You reached Max Digits!")]
        [MinLength(2, ErrorMessage = "Please write longer name!")]
        public string LoginName { get; set; }
        [Required]
        
        public byte[] PasswordHash { get; set; }
        [Required(ErrorMessage = "Please write a name!")]
        [MaxLength(30, ErrorMessage = "You reached Max Digits!")]
        [MinLength(2, ErrorMessage = "Please write longer name!")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please write a name!")]
        [MaxLength(30, ErrorMessage = "You reached Max Digits!")]
        [MinLength(2, ErrorMessage = "Please write longer name!")]
        public string LastName { get; set; }
    }
}
