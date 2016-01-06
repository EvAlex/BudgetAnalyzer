using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            Operations = new List<AccountOperation>();
            CorrespondentOperations = new List<AccountOperation>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Bank")]
        public int? BankId { get; set; }

        public virtual Bank Bank { get; set; }

        public BankAccountType Type { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AccountOperation> Operations { get; set; }

        public virtual ICollection<AccountOperation> CorrespondentOperations { get; set; }
    }
}
