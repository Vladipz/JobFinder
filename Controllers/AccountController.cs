using JobFinder.Data;
using JobFinder.Models;
using JobFinder.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JobFinder.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationDbContext _context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
        }
		public IActionResult Index()
		{
			return View();
		}
		
        public IActionResult Login()
		{
			var response = new LoginViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVm)
		{
			if (!ModelState.IsValid)
				return View(loginVm);


			var user = await _userManager.FindByEmailAsync(loginVm.Email);
			if (user != null)
			{
				//User is found
				var passwordCheck = _userManager.CheckPasswordAsync(user,loginVm.Password);
				if (passwordCheck.Result)
				{
					//passwoed correct
					var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Vacancy");
					}
				}
				//passwoed is incorrect

				TempData["Error"] = "Неправильно введені дані. Будь ласка, спробуйте ще";
				return View(loginVm);
			}
			//User not found 
			TempData["Error"] = "Користувач незнайдений. Будь ласка, спробуйте ще";
			return View(loginVm);
		}
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerVM)
		{
			if (!ModelState.IsValid) return View(registerVM);
			var user = await _userManager.FindByEmailAsync(registerVM.Email);
			if (user != null)
			{
                TempData["Error"] = "Ця електронна адреса вже зареєстрована";
				return View(registerVM);
            }
			var newUser = new User();

            if (registerVM.Role == Data.Enum.Roles.Employer)
			{
				newUser = new Employer()
				{
					Email = registerVM.Email,
					UserName = registerVM.Email,
					Name = registerVM.Name,
					Surname = registerVM.Surname

				};
			}
			else if (registerVM.Role == Data.Enum.Roles.Searcher)
			{
				newUser = new Searcher()
				{
					Email = registerVM.Email,
					UserName = registerVM.Email,
                    Name = registerVM.Name,
                    Surname = registerVM.Surname
                };

			}
			else
			{
                TempData["Error"] = "Неправильно вказаний тип аккаута";
                return View(registerVM);
            }

			var newUserResponce = await _userManager.CreateAsync(newUser, registerVM.Password);
			if (newUserResponce.Succeeded)
			{
				if (newUser is Searcher)
					await _userManager.AddToRoleAsync(newUser, UserRoles.Searcher);
				else if (newUser is Employer)
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Employer);
            }
			return RedirectToAction("Index","Vacancy");
        }
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Vacancy");
		}
    }
}
