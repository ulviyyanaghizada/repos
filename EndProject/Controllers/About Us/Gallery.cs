﻿using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class Gallery : Controller
    {
		readonly AppDbContext _context;

		public Gallery(AppDbContext context)
		{
			_context = context;

		}
		public IActionResult Index()
        {
			HomeVM home = new HomeVM
			{
				Brands = _context.Brands,

			};

			return View(home);
		}
    }
}