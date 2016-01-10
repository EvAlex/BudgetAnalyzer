using BudgetAnalyzer.Models;
using BudgetAnalyzer.Tests.Mocks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace BudgetAnalyzer.Tests.Model
{
    public class ApplicationDbContextTests : TestsBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                   .AddInMemoryDatabase()
                   .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
        }

        [Fact]
        public async Task SaveFileUploadAsync_SavesFileUpload()
        {
            //  arrange
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var file = new ExcelFormFileMock(XDocument.Parse("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><worksheet></worksheet>"));
            int uploadsBefore = await db.FileUploads.CountAsync();

            //  act
            await db.SaveFileUploadAsync(file);
            int uploadsAfter = await db.FileUploads.CountAsync();

            //  assert
            Assert.Equal(uploadsBefore + 1, uploadsAfter);
        }

        [Fact]
        public async Task SaveFileUploadAsync_SavesFileUploadCorrectly()
        {
            //  arrange
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var file = new ExcelFormFileMock(
                XDocument.Parse("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><worksheet></worksheet>"),
                "test-upload-123.xlsx");
            int uploadsBefore = await db.FileUploads.CountAsync();

            //  act
            var upload = await db.SaveFileUploadAsync(file);

            //  assert
            Assert.NotEqual(0, upload.Id);
            Assert.Equal("test-upload-123.xlsx", upload.FileName);
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", upload.ContentType);
            Assert.Equal(file.Length, upload.Content.Length);
            Assert.InRange(upload.CreatedAt.Ticks, DateTimeOffset.Now.AddSeconds(-1).Ticks, DateTimeOffset.Now.Ticks);
        }
    }
}
