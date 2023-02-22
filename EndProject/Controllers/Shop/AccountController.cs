using EndProject.Models.AppUser;
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

            var res = await _userManager.CreateAsync(user, "SuperAdmin");
            return Ok(res);
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole superAdmin = new IdentityRole("SuperAdmin");
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole member = new IdentityRole("Member");
            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(superAdmin);
            await _roleManager.CreateAsync(member);
            var res = await _roleManager.CreateAsync(superAdmin);
            return Ok(res);
        }

        public async Task<IActionResult> AddRole()
        {
            AppUser user = await _userManager.FindByNameAsync("AdilAghayev");

            var res = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            return Ok(res);
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
