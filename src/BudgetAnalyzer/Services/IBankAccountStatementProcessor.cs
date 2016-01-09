using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Services
{
    public interface IBankAccountStatementProcessor
    {
        void StartProcessing(int accountStatementId);

        void Process(int accountStatementId);
    }
}
