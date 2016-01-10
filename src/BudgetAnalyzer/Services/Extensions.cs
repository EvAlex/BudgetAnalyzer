using BudgetAnalyzer.Services.StatementParsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Services
{
    public static class Extensions
    {
        public static void AddStatementProcessing(this IServiceCollection services)
        {
            services.AddTransient<IBankAccountStatementProcessor, BankAccountStatementProcessor>();
            services.AddTransient<IBankAccountStatementParser, SberbankStatementParser>();
            services.AddTransient<IBankAccountStatementParser, TinkoffBankStatementParser>();
            services.AddTransient<AggregateStatementParser>();
        }
    }
}
