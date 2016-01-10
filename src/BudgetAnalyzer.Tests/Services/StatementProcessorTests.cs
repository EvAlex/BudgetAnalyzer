using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetAnalyzer.Services;
using Xunit;

namespace BudgetAnalyzer.Tests.Services
{
    public class StatementProcessorTests : TestsBase
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
            var processor = serviceProvider.GetRequiredService<IBankAccountStatementProcessor>();

            //  assert
            Assert.IsType(typeof(BankAccountStatementProcessor), processor);
        }
    }
}
