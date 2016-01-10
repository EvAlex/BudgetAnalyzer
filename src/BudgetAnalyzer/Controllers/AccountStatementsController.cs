using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Models;
using BudgetAnalyzer.ViewModels.AccountStatements;
using System;
using BudgetAnalyzer.Services;
using System.Diagnostics;

namespace BudgetAnalyzer.Controllers
{
    public class AccountStatementsController : Controller
    {
        private readonly ApplicationDbContext context;

        private readonly IBankAccountStatementProcessor statementProcessor;

        public AccountStatementsController(
            ApplicationDbContext context,
            IBankAccountStatementProcessor statementProcessor)
        {
            this.context = context;
            this.statementProcessor = statementProcessor;
        }

        // GET: AccountStatements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.AccountStatements.Include(a => a.BankAccount).Include(a => a.FileUpload);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AccountStatements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountStatement accountStatement = await context.AccountStatements.SingleAsync(m => m.Id == id);
            if (accountStatement == null)
            {
                return HttpNotFound();
            }

            return View(accountStatement);
        }

        // GET: AccountStatements/Create
        public IActionResult Create(int? accountId = null)
        {
            var viewModel = new AccountStatementViewModel { BankAccountId = accountId };
            ViewData["BankAccountId"] = new SelectList(context.BankAccounts, "Id", "Name");
            return View(viewModel);
        }

        // POST: AccountStatements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountStatementViewModel model)
        {
            if (ModelState.IsValid)
            {
                IFileUpload upload;
                try
                {
                    upload = await context.SaveFileUploadAsync(model.Attachment);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to save uploaded file. Please upload it again later.", ex);
                }
                var statement = new AccountStatement
                {
                    BankAccountId = model.BankAccountId.Value,
                    FileUploadId = upload.Id,
                    ProcessedAt = DateTimeOffset.Now
                };
                context.AccountStatements.Add(statement);
                await context.SaveChangesAsync();

                statementProcessor.StartProcessing(statement.Id);

                return RedirectToAction("Index");
            }
            ViewData["BankAccountId"] = new SelectList(context.BankAccounts, "Id", "Name", model.BankAccountId);
            return View(model);
        }

        // GET: AccountStatements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountStatement accountStatement = await context.AccountStatements.SingleAsync(m => m.Id == id);
            if (accountStatement == null)
            {
                return HttpNotFound();
            }
            ViewData["BankAccountId"] = new SelectList(context.BankAccounts, "Id", "BankAccount", accountStatement.BankAccountId);
            ViewData["FileUploadId"] = new SelectList(context.FileUploads, "Id", "FileUpload", accountStatement.FileUploadId);
            return View(accountStatement);
        }

        // POST: AccountStatements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountStatement accountStatement)
        {
            if (ModelState.IsValid)
            {
                context.Update(accountStatement);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BankAccountId"] = new SelectList(context.BankAccounts, "Id", "BankAccount", accountStatement.BankAccountId);
            ViewData["FileUploadId"] = new SelectList(context.FileUploads, "Id", "FileUpload", accountStatement.FileUploadId);
            return View(accountStatement);
        }

        // GET: AccountStatements/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountStatement accountStatement = await context.AccountStatements.SingleAsync(m => m.Id == id);
            if (accountStatement == null)
            {
                return HttpNotFound();
            }

            return View(accountStatement);
        }

        // POST: AccountStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AccountStatement accountStatement = await context.AccountStatements.SingleAsync(m => m.Id == id);
            context.AccountStatements.Remove(accountStatement);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
