using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Services;
using Microsoft.AspNet.Http;
using System.IO;

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

            builder.Entity<AccountStatement>().ToTable("AccountStatements");

            builder.Entity<FileUpload>().ToTable("FileUploads");

        }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<AccountOperation> AccountOperations { get; set; }

        public DbSet<AccountStatement> AccountStatements { get; set; }

        public DbSet<FileUpload> FileUploads { get; set; }

        public async Task<IFileUpload> SaveFileUploadAsync(IFormFile file)
        {
            string filename = file.ContentDisposition.Split(';')
                .Select(x => x.Trim())
                .Where(x => x.StartsWith("filename="))
                .Select(x => x.Substring(9).Trim('"'))
                .First();
            using (var stream = file.OpenReadStream())
            {
                byte[] buffer = new byte[stream.Length];
                if (await stream.ReadAsync(buffer, 0, buffer.Length) != buffer.Length)
                    throw new IOException("Failed to read uploaded file stream.");
                var upload = new FileUpload
                {
                    Content = buffer,
                    ContentType = file.ContentType,
                    FileName = filename
                };
                FileUploads.Add(upload);
                await SaveChangesAsync();

                return upload;
            }
        }
    }
}
