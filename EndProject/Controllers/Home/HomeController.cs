using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Home
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM { 

                Entries = _context.Entries , 
                Chooses = _context.Chooses,
                Agencies=_context.Agencies,
                Brands=_context.Brands
            
            };

            return View(home);
        }
        public IActionResult GetEmployee(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if(employee == null) return NotFound();

            return PartialView("_EmployeeModalPartial",employee);
        }
    }
}
