﻿using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class TeamMember : Controller
    {
        readonly AppDbContext _context;

        public TeamMember(AppDbContext context)
        {
            _context = context;

        }
        //public IActionResult Index()
        //{
        //    HomeVM home = new HomeVM
        //    {
        //        Employees = _context.Employees.Include(e => e.Position).ToList()
        //    };
        //    return View(home);
        //}
        public IActionResult EmployeeDetail(int id)
        {


            HomeVM home = new HomeVM
            {
                Employee = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == id),
                Employees= _context.Employees.Include(e => e.Position).ToList()

            };

            return View(home);
        }
    }
}
