using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.ViewModels
{
    public class StatementViewModel
    {
        public IFormFile Attachment { get; set; }

        public int BankAccountId { get; set; }
    }
}
