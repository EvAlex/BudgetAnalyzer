using BudgetAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.ViewModels.AccountOperations
{
    public class AccountOperationsIndexViewModel
    {
        public BankAccount[] Accounts { get; set; }

        public BankAccount CurrentAccount { get; set; }

        public AccountOperation[] CurrentAccountOperations { get; set; }
    }
}
