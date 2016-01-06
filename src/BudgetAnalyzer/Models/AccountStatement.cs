using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Models
{
    public class AccountStatement
    {
        public AccountStatement()
        {
            Operations = new List<AccountOperation>();
        }

        public int Id { get; set; }

        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual ICollection<AccountOperation> Operations { get; set; }

        public int FileUploadId { get; set; }

        public virtual FileUpload FileUpload { get; set; }

        public DateTimeOffset? ProcessedAt { get; set; }
    }
}
