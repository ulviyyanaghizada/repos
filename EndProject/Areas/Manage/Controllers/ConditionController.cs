using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
	[Area("Manage")]

	public class ConditionController : Controller
	{
		readonly AppDbContext _context;
		public ConditionController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View(_context.Conditions.ToList());
		}
		public IActionResult Delete(int id)
		{

			Condition condition = _context.Conditions.Find(id);
			if (condition is null)
			{
				return NotFound();
			}
			_context.Conditions.Remove(condition);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Condition condition)
		{
			if (_context.Conditions.ToList().Count >= 8) return RedirectToAction(nameof(Index));
			if (!ModelState.IsValid)
			{
				return View();
			}

			_context.Conditions.Add(condition);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Update(int? id)
		{
			if (id is null || id <= 0) return BadRequest();
			Condition condition = _context.Conditions.Find(id);
			if (condition is null) return NotFound();
			return View(condition);
		}
		[HttpPost]
		public IActionResult Update(int? id, Condition condition)
		{
			if (id is null || id == 0) return BadRequest();

			if (!ModelState.IsValid)
			{
				return View();
			}
			Condition exist = _context.Conditions.FirstOrDefault(c => c.Id == id);
			if (exist is null) return NotFound();
			exist.Description = condition.Description;
			exist.Title = condition.Title;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

	}
}
