using BudgetAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Services.StatementParsers
{
    public class SberbankStatementParser : IBankAccountStatementParser
    {
        public AccountStatement[] Parse(Stream statementFile, string contentType, string bankName)
        {
            throw new NotImplementedException();
        }

        public bool CanParse(Stream statementFile, string contentType, string bankName)
        {
            throw new NotImplementedException();
        }
    }
}
