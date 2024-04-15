using JobFinder.Interfaces;
using JobFinder.Models;
using JobFinder.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobFinder.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		[HttpGet("users")]
		public async Task<IActionResult> Index()
		{
			var users = await _userRepository.GetAllUsers();
			List<UserViewModel> result = new List<UserViewModel>();
			foreach (var user in users)
			{
				var userViewModel = new UserViewModel
				{
					Id = user.Id,
					Name = user.Name,
					Surname = user.Surname,
					Email = user.Email,
				};
				result.Add(userViewModel);
			}
			return View(result);
		}
		public async Task<IActionResult> Detail(string id)
		{
            var author = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Email = author.Email,
				IsSubscribed = false
            };

            if (User.Identity.IsAuthenticated)
			{
				var authenticatedUser = await _userRepository.GetUserByName(User.Identity.Name);
				var authenticatedUserSubscriptions = await _userRepository
					.GetSubscriptionsByUserId(authenticatedUser.Id);

                if (authenticatedUserSubscriptions.Contains(author))
				{
                    userDetailViewModel.IsSubscribed = true;

                }
            }
			
			
			return View(userDetailViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> AddSubscriber(UserDetailViewModel author)
		{

			if (User.Identity.IsAuthenticated)
			{
				var user = await _userRepository.GetUserByName(User.Identity.Name);
				if (user != null)
				{
					var success = await _userRepository.AddSubscriber(author.Id, user.Id);
					if (success)
					{
						return RedirectToAction("Detail", new
						{
							id = author.Id
						});
					}
				}
			}
			else
			{
                return RedirectToAction("Login", "Account");
            }
			

			return BadRequest("Failed to add subscriber. User or subscriber not found.");

        }

		[HttpPost]
		public async Task<IActionResult> RemoveSubscriber(UserDetailViewModel author)
		{
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUserByName(User.Identity.Name);
                if (user != null)
                {
					var success = await _userRepository.RemoveSubscriber(author.Id, user.Id);
                    if (success)
                    {
                        return RedirectToAction("Detail", new
                        {
                            id = author.Id
                        });
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


            return BadRequest("Failed to add subscriber. User or subscriber not found.");
        }



    }
}
