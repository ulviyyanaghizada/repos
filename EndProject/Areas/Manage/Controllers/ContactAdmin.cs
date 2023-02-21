using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ContactAdmin : Controller
    {
        private readonly AppDbContext _context;

        public ContactAdmin(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {

            return View(_context.ContactUs.ToList());;
        }

        public IActionResult Seen(int Id)
        {
            ContactUs contact = _context.ContactUs.FirstOrDefault(x => x.Id == Id);
            if (contact == null) { return BadRequest(); }

            contact.IsSeen = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Ignore(int Id)
        {
            ContactUs contact = _context.ContactUs.FirstOrDefault(x => x.Id == Id);
            if (contact == null) { return BadRequest(); }

            _context.ContactUs.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            ContactUs contact = _context.ContactUs.FirstOrDefault(x => x.Id == Id);
            if (contact == null) { return BadRequest(); }
            return View(contact);
        }


    }
}
