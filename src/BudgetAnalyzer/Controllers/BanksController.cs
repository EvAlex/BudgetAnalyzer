using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BudgetAnalyzer.Models;

namespace BudgetAnalyzer.Controllers
{
    public class BanksController : Controller
    {
        private ApplicationDbContext _context;

        public BanksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Banks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banks.ToListAsync());
        }

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Bank bank = await _context.Banks.SingleAsync(m => m.Id == id);
            if (bank == null)
            {
                return HttpNotFound();
            }

            return View(bank);
        }

        // GET: Banks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bank bank)
        {
            if (ModelState.IsValid)
            {
                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bank);
        }

        // GET: Banks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Bank bank = await _context.Banks.SingleAsync(m => m.Id == id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bank bank)
        {
            if (ModelState.IsValid)
            {
                _context.Update(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bank);
        }

        // GET: Banks/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Bank bank = await _context.Banks.SingleAsync(m => m.Id == id);
            if (bank == null)
            {
                return HttpNotFound();
            }

            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Bank bank = await _context.Banks.SingleAsync(m => m.Id == id);
            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
