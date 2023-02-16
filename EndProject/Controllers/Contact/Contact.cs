using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using EndProject.Models;


namespace EndProject.Controllers.Contact
{
	public class Contact : Controller
	{
        readonly AppDbContext _context;

        public Contact(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
		{
            HomeVM home = new HomeVM
            {
                OfficeMaps = _context.OfficeMaps,
                ContinentInfos = _context.ContinentInfos,

            };

            return View(home);
        }
	}
}
