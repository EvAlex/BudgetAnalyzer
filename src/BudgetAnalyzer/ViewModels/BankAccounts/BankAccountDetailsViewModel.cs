using BudgetAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.ViewModels.BankAccounts
{
    public class BankAccountDetailsViewModel
    {
        public BankAccountDetailsViewModel(BankAccount account, BankAccountDetailsTab activeTab, AccountStatement[] statements = null)
        {
            Account = account;
            ActiveTab = activeTab;
            Statements = statements;
        }

        public BankAccount Account { get; }

        public BankAccountDetailsTab ActiveTab { get; }

        public AccountStatement[] Statements { get; }
    }
}
