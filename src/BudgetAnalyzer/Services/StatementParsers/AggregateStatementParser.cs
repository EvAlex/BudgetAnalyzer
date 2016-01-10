using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetAnalyzer.Models;

namespace BudgetAnalyzer.Services.StatementParsers
{
    public class AggregateStatementParser : IBankAccountStatementParser
    {
        public IReadOnlyCollection<IBankAccountStatementParser> Parsers
        {
            get { return parsers.ToList().AsReadOnly(); }
        }
        private readonly IEnumerable<IBankAccountStatementParser> parsers;

        public AggregateStatementParser(IEnumerable<IBankAccountStatementParser> parsers)
        {
            this.parsers = parsers;
        }

        public AccountStatement[] Parse(Stream statementFile, string contentType, string bankName)
        {
            AccountStatement[] res = null;

            var candidates = GetRequiredParsers(statementFile, contentType, bankName);
            var exceptions = new List<Exception>();
            foreach (var p in candidates)
            {
                try
                {
                    res = p.Parse(statementFile, contentType, bankName);
                    break;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            if (res == null)
                throw new AggregateException(
                    "Failed to parse Bank Account Statement with all evailable parsers. See inner exceptions for details.", 
                    exceptions);

            return res;
        }

        public bool CanParse(Stream statementFile, string contentType, string bankName)
        {
            return GetRequiredParsers(statementFile, contentType, bankName).Any();
        }

        private IEnumerable<IBankAccountStatementParser> GetRequiredParsers(Stream statementFile, string contentType, string bankName)
        {
            return parsers.Where(p =>
            {
                try
                {
                    return p.CanParse(statementFile, contentType, bankName);
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
