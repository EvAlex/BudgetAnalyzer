using System.IO;
using BudgetAnalyzer.Models;

namespace BudgetAnalyzer.Services.StatementParsers
{
    public interface IBankAccountStatementParser
    {
        AccountStatement[] Parse(Stream statementFile, string contentType);

        bool CanParse(Stream statementFile, string contentType);
    }
}