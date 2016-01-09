using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.ViewModels.AccountStatements
{
    public class AccountStatementViewModel : IValidatableObject
    {
        [Display(Name = "Bank Account")]
        public int? BankAccountId { get; set; }

        public IFormFile Attachment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!BankAccountId.HasValue)
                yield return new ValidationResult("Bank Account should be specified", new[] { nameof(BankAccountId) });
        }
    }
}
