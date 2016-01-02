using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace BudgetAnalyzer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Bank>().ToTable("Banks");

            builder.Entity<BankAccount>().ToTable("BankAccounts");
            builder.Entity<BankAccount>()
                .HasOne(e => e.User)
                .WithMany(e => e.Accounts)
                .HasForeignKey(e => e.UserId);

            builder.Entity<AccountOperation>().ToTable("AccountOperations");
            builder.Entity<AccountOperation>()
                .HasOne(e => e.Account)
                .WithMany(e => e.Operations)
                .HasForeignKey(e => e.AccountId);
            builder.Entity<AccountOperation>()
                .HasOne(e => e.CorrespondentAccount)
                .WithMany(e => e.CorrespondentOperations)
                .HasForeignKey(e => e.CorrespondentAccountId);
        }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<AccountOperation> AccountOperations { get; set; }
    }
}
