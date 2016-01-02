using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Models
{
    public class AccountOperation
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public BankAccount Account { get; set; }

        public int CorrespondentAccountId { get; set; }

        public BankAccount CorrespondentAccount { get; set; }

        public double Ammount { get; set; }

        public DateTime Time { get; set; }

        public string Name { get; set; }

        public double Saldo { get; set; }
    }
}
