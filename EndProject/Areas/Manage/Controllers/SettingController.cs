using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        readonly AppDbContext _context;
        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Settings.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Settings.Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Setting exist = _context.Settings.FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            UpdateSettingVM updateSetting = new UpdateSettingVM
            {
                Value = exist.Value,
                Key = exist.Key
            };
            return View(updateSetting);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateSettingVM updateSetting)
        {
            if (id is null || id == 0) return BadRequest();
            if (!ModelState.IsValid) return View();
            Setting exist = _context.Settings.FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            exist.Value = updateSetting.Value;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
