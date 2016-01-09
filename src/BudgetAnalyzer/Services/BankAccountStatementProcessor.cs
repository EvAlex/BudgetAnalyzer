using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Services
{
    public class BankAccountStatementProcessor : IBankAccountStatementProcessor
    {
        public void StartProcessing(int accountStatementId)
        {
            Task.Factory.StartNew(() => Process(accountStatementId));
        }

        public void Process(int accountStatementId)
        {
            throw new NotImplementedException();
        }
    }
}
