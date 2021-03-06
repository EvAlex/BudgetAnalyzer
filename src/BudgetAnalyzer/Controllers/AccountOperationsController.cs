using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Models;
using Microsoft.AspNet.Http;
using BudgetAnalyzer.ViewModels;
using System;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using BudgetAnalyzer.ViewModels.AccountOperations;

namespace BudgetAnalyzer.Controllers
{
    [Authorize]
    public class AccountOperationsController : Controller
    {
        private ApplicationDbContext _context;

        public AccountOperationsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AccountOperations
        public async Task<IActionResult> Index(int? accountId = null)
        {
            var userId = User.GetUserId();
            var allAccounts = _context.BankAccounts.Where(a => a.UserId == userId);
            var currentAccount = accountId.HasValue
                ? await allAccounts.FirstAsync(a => a.Id == accountId.Value)
                : await allAccounts.FirstOrDefaultAsync();
            var currentAccountOperations = currentAccount == null
                ? new AccountOperation[0]
                : await _context.AccountOperations
                    .Where(o => o.AccountId == currentAccount.Id)
                    .Include(a => a.Account)
                    .Include(a => a.CorrespondentAccount)
                    .ToArrayAsync();
            return View(new AccountOperationsIndexViewModel
            {
                 Accounts = await allAccounts.ToArrayAsync(),
                 CurrentAccount = currentAccount,
                 CurrentAccountOperations = currentAccountOperations
            });
        }

        // GET: AccountOperations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountOperation accountOperation = await _context.AccountOperations.SingleAsync(m => m.Id == id);
            if (accountOperation == null)
            {
                return HttpNotFound();
            }
            if (accountOperation.Account.UserId != User.GetUserId())
                return HttpNotFound("AccountOperation with specified Id doen't exist. Or does it? Maybe it belongs to someone else? Who knows...");

            return View(accountOperation);
        }

        // GET: AccountOperations/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.BankAccounts, "Id", "Account");
            ViewData["CorrespondentAccountId"] = new SelectList(_context.BankAccounts, "Id", "CorrespondentAccount");
            return View();
        }

        // POST: AccountOperations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountOperation accountOperation)
        {
            if (ModelState.IsValid)
            {
                _context.AccountOperations.Add(accountOperation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AccountId"] = new SelectList(_context.BankAccounts, "Id", "Account", accountOperation.AccountId);
            ViewData["CorrespondentAccountId"] = new SelectList(_context.BankAccounts, "Id", "CorrespondentAccount", accountOperation.CorrespondentAccountId);
            return View(accountOperation);
        }

        // GET: AccountOperations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountOperation accountOperation = await _context.AccountOperations.SingleAsync(m => m.Id == id);
            if (accountOperation == null)
            {
                return HttpNotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.BankAccounts, "Id", "Account", accountOperation.AccountId);
            ViewData["CorrespondentAccountId"] = new SelectList(_context.BankAccounts, "Id", "CorrespondentAccount", accountOperation.CorrespondentAccountId);
            return View(accountOperation);
        }

        // POST: AccountOperations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountOperation accountOperation)
        {
            if (ModelState.IsValid)
            {
                _context.Update(accountOperation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AccountId"] = new SelectList(_context.BankAccounts, "Id", "Account", accountOperation.AccountId);
            ViewData["CorrespondentAccountId"] = new SelectList(_context.BankAccounts, "Id", "CorrespondentAccount", accountOperation.CorrespondentAccountId);
            return View(accountOperation);
        }

        // GET: AccountOperations/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountOperation accountOperation = await _context.AccountOperations.SingleAsync(m => m.Id == id);
            if (accountOperation == null)
            {
                return HttpNotFound();
            }

            return View(accountOperation);
        }

        // POST: AccountOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AccountOperation accountOperation = await _context.AccountOperations.SingleAsync(m => m.Id == id);
            _context.AccountOperations.Remove(accountOperation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: AccountOperations/UploadStatement
        public async Task<IActionResult> UploadStatement()
        {
            return View();
        }

        // POST: AccountOperations/UploadStatement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadStatement(StatementViewModel statement)
        {
            try
            {
                var upload = await _context.SaveFileUploadAsync(statement.Attachment);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save uploaded file. Please upload it again later.", ex);
            }

            return View("UploadStatementResult", statement);
        }
    }
}
