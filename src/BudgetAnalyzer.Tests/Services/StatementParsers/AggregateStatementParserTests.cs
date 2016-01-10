using BudgetAnalyzer.Services;
using BudgetAnalyzer.Services.StatementParsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BudgetAnalyzer.Tests.Services.StatementParsers
{
    public class AggregateStatementParserTests : TestsBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddStatementProcessing();
        }

        [Fact]
        public void StatementProcessor_IoC()
        {
            //  arrange

            //  act
            var parser = serviceProvider.GetRequiredService<AggregateStatementParser>();

            //  assert
            Assert.IsType(typeof(AggregateStatementParser), parser);
            Assert.NotEmpty(parser.Parsers);
            Assert.Single(parser.Parsers.OfType<SberbankStatementParser>());
            Assert.Single(parser.Parsers.OfType<TinkoffBankStatementParser>());
        }
    }
}
