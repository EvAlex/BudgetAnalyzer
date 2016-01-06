using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Models;

namespace BudgetAnalyzer.Controllers
{
    public class AccountStatementsController : Controller
    {
        private ApplicationDbContext _context;

        public AccountStatementsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AccountStatements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AccountStatements.Include(a => a.BankAccount).Include(a => a.FileUpload);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AccountStatements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountStatement accountStatement = await _context.AccountStatements.SingleAsync(m => m.Id == id);
            if (accountStatement == null)
            {
                return HttpNotFound();
            }

            return View(accountStatement);
        }

        // GET: AccountStatements/Create
        public IActionResult Create()
        {
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "BankAccount");
            ViewData["FileUploadId"] = new SelectList(_context.FileUploads, "Id", "FileUpload");
            return View();
        }

        // POST: AccountStatements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountStatement accountStatement)
        {
            if (ModelState.IsValid)
            {
                _context.AccountStatements.Add(accountStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "BankAccount", accountStatement.BankAccountId);
            ViewData["FileUploadId"] = new SelectList(_context.FileUploads, "Id", "FileUpload", accountStatement.FileUploadId);
            return View(accountStatement);
        }

        // GET: AccountStatements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            AccountStatement accountStatement = await _context.AccountStatements.SingleAsync(m => m.Id == id);
            if (accountStatement == null)
            {
                return HttpNotFound();
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "BankAccount", accountStatement.BankAccountId);
            ViewData["FileUploadId"] = new SelectList(_context.FileUploads, "Id", "FileUpload", accountStatement.FileUploadId);
            return View(accountStatement);
        }

        // POST: AccountStatements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountStatement accountStatement)
        {
            if (ModelState.IsValid)
            {
                _context.Update(accountStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "BankAccount", accountStatement.BankAccountId);
            ViewData["FileUploadId"] = new SelectList(_context.FileUploads, "Id", "FileUpload", accountStatement.FileUploadId);
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

            AccountStatement accountStatement = await _context.AccountStatements.SingleAsync(m => m.Id == id);
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
            AccountStatement accountStatement = await _context.AccountStatements.SingleAsync(m => m.Id == id);
            _context.AccountStatements.Remove(accountStatement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
