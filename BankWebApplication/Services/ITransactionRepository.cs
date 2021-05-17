﻿using BankWebbApp.Data;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BankWebbApp.Repository
{
    public interface ITransactionRepository
    {

        List<Transaction> GetAllTransactions();
    }
}