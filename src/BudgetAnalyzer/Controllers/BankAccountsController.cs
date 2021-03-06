using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Models;
using Microsoft.AspNet.Authorization;
using BudgetAnalyzer.ViewModels.BankAccounts;
using System.Security.Claims;

namespace BudgetAnalyzer.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BankAccounts.Include(b => b.Bank).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id, BankAccountDetailsTab tab = BankAccountDetailsTab.AccountOperations)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var userId = User.GetUserId();
            BankAccount bankAccount = await _context.BankAccounts
                .Include(a => a.Operations)
                .SingleAsync(m => m.Id == id && m.UserId == userId);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }

            var statements = tab == BankAccountDetailsTab.UploadedStatements
                ? await _context.AccountStatements.Where(s => s.BankAccountId == bankAccount.Id).ToArrayAsync()
                : null;

            var viewModel = new BankAccountDetailsViewModel(bankAccount, tab, statements);

            return View(viewModel);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
            return View();
        }

        // POST: BankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                _context.BankAccounts.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", bankAccount.BankId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            BankAccount bankAccount = await _context.BankAccounts.SingleAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", bankAccount.BankId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Update(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", bankAccount.BankId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            BankAccount bankAccount = await _context.BankAccounts.SingleAsync(m => m.Id == id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BankAccount bankAccount = await _context.BankAccounts.SingleAsync(m => m.Id == id);
            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
