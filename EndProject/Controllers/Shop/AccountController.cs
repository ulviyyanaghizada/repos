using EndProject.Models.AppUser;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
  

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
   
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser()
        {
            AppUser user = new AppUser
            {
                FirstName = "Adil",
                LastName = "Aghayev",
                Email = "ulviyyanagizade@gmail.com",
                UserName = "AdilAghayev"
            };

            //var res = await _userManager.CreateAsync(user, "SuperAdmin");

            var res = await _userManager.CreateAsync(user, "Test-123");
            return Ok(res.Succeeded);
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole superAdmin = new IdentityRole("SuperAdmin");
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole member = new IdentityRole("Member");
            await _roleManager.CreateAsync(superAdmin);
            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(member);

            return Ok();
        }


        public async Task<IActionResult> AddRole()
        {
            AppUser user = await _userManager.FindByNameAsync("AdilAghayev");

            var res = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            return Ok(res);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser mail = await _userManager.FindByEmailAsync(model.EmailOrUsername);
            AppUser username = await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (mail is null && username is null)
            {
                if (model.EmailOrUsername.Contains("@")) ModelState.AddModelError("", "Email or Password is incorrect");
                else ModelState.AddModelError("", "Username or Password is incorrect");

                return View();
            }

            var res = await _signInManager.PasswordSignInAsync(mail != null ? mail : username, model.Password, model.RememberMe, false);
            if (!res.Succeeded)
            {
                if (model.EmailOrUsername.Contains("@")) ModelState.AddModelError("", "Email or Password is incorrect");
                else ModelState.AddModelError("", "Username or Password is incorrect");

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                ModelState.AddModelError("UserName", "This Username has taken!");
                return View();
            }

            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "This Email has taken!");
                return View();
            }

            user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username
            };

            var res = await _userManager.CreateAsync(user, model.ConfirmPassword);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
