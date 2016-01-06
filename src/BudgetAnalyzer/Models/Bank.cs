using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Models
{
    public class Bank
    {
        public Bank()
        {
            Accounts = new List<BankAccount>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BankAccount> Accounts { get; set; }
    }
}
