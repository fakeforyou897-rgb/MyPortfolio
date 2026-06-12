using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.ContactMessages
                                   .OrderByDescending(m => m.CreatedAt)
                                   .ToList();
            return View(messages);
        }

        public IActionResult Details(Guid id)
        {
            var message = _context.ContactMessages.FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();
            return View(message);
        }

        public IActionResult Delete(Guid id)
        {
            var message = _context.ContactMessages.FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();
            return View(message);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var message = _context.ContactMessages.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
