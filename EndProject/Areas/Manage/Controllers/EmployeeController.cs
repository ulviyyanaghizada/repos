using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.EntityFrameworkCore;
using EndProject.Utilities.Extensions;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.Include(e => e.Position));
        }
        public IActionResult Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM createEmployee)
        {
            var image = createEmployee.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!_context.Positions.Any(p => p.Id == createEmployee.PositionId))
            {
                ModelState.AddModelError("PositionId", "Bu Id'li vəzifə yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            Employee employee = new Employee()
            {
                Name = createEmployee.Name,
                Surname = createEmployee.Surname,
                Description = createEmployee.Description,
                About= createEmployee.About,
                PhoneNumber= createEmployee.PhoneNumber,
                Mail= createEmployee.Mail,
                FacebookLink=createEmployee.FacebookLink,
                InstagramLink=createEmployee.InstagramLink,
                TwitterLink=createEmployee.TwitterLink,
                YoutubeLink=createEmployee.YoutubeLink, 
                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                PositionId = createEmployee.PositionId
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Employee exist = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            UpdateEmployeeVM update = new UpdateEmployeeVM()
            {
                Name = exist.Name,
                Surname = exist.Surname,
                Description = exist.Description,
                About=exist.About,
                PhoneNumber = exist.PhoneNumber,
                FacebookLink=exist.FacebookLink,
                Mail=exist.Mail,
                InstagramLink=exist.InstagramLink,
                TwitterLink=exist.TwitterLink,
                YoutubeLink=exist.YoutubeLink,
                ImageUrl = exist.ImageUrl,
                PositionId = exist.PositionId
            };
            ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
            ViewBag.Image = exist.ImageUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateEmployeeVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var image = update.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!_context.Positions.Any(p => p.Id == update.PositionId))
            {
                ModelState.AddModelError("PositionId", "Bu Id'li vəzifə yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
                ViewBag.Image = _context.Employees.FirstOrDefault(e => e.Id == id).ImageUrl;
                return View();
            }
            Employee exist = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
                exist.ImageUrl = newImage;
            }
            exist.Name = update.Name;
            exist.Surname = update.Surname;
            exist.PositionId = update.PositionId;
            exist.Description = update.Description;
            exist.About = update.About;
            exist.Mail = update.Mail;
            exist.PhoneNumber = update.PhoneNumber;
            exist.FacebookLink= update.FacebookLink;
            exist.TwitterLink= update.TwitterLink;
            exist.InstagramLink= update.InstagramLink;
            exist.YoutubeLink= update.YoutubeLink;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Employee exist = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            _context.Employees.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
