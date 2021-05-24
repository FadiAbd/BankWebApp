using BankWebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.ViewModels
{
    public class WithdrawalViewModel
    {
        public int TransactionId { get; set; }

        //[Range(1, int.MinValue, ErrorMessage = "choose the withdrawal targeting Account")]
        public int AccountId { get; set; }

        [Required]
        [Remote("overrun","Withdrawal", AdditionalFields = "AccountId")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        //[Required]
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
