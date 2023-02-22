using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EndProject.Models;
using EndProject.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EndProject.Controllers.Contact
{
	public class ContactController : Controller
	{
        readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
		{

            return View();
        }
        
		[HttpPost]
        public IActionResult postContact(ContactVM model)
        {
            if (model is null) { return BadRequest(); }
            if (!ModelState.IsValid) return RedirectToAction("Index",model);

            ContactUs contact = new ContactUs
            {
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                subject = model.subject,
                PhoneNumber = model.PhoneNumber,

            };

            _context.ContactUs.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

