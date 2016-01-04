using System.ComponentModel.DataAnnotations;

namespace BudgetAnalyzer.Models
{
    public enum BankAccountType
    {
        [Display(Name = "-- Not specified --")]
        NotSpecified = 0,
        Deposit = 1,
        Credit = 2
    }
}