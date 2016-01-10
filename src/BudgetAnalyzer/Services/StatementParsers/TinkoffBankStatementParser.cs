using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetAnalyzer.Models;

namespace BudgetAnalyzer.Services.StatementParsers
{
    public class TinkoffBankStatementParser : IBankAccountStatementParser
    {
        public bool CanParse(Stream statementFile, string contentType, string bankName)
        {
            throw new NotImplementedException();
        }

        public AccountStatement[] Parse(Stream statementFile, string contentType, string bankName)
        {
            throw new NotImplementedException();
        }
    }
}
