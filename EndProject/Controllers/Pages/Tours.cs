﻿using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class Tours : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
