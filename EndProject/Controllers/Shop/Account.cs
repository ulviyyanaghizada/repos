﻿using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
