using BudgetAnalyzer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Tests
{
    public abstract class TestsBase
    {
        protected readonly IServiceProvider serviceProvider;

        public TestsBase()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            this.serviceProvider = services.BuildServiceProvider();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddStatementProcessing();
        }
    }
}
