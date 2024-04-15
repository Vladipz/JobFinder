using JobFinder.Interfaces;
using JobFinder.Models;
using JobFinder.Repository;
using JobFinder.ViewModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
	public class ResumeController : Controller
	{

		private readonly IResumeRepository _resumeRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ResumeController(IResumeRepository resumeRepository, IHttpContextAccessor httpContextAccessor)
		{
			_resumeRepository = resumeRepository;
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Detail(int id)
		{
			var resume = await _resumeRepository.GetByIdAsync(id);
			return View(resume);
		}
		public IActionResult Create()
		{
			var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			var createResumeViewModel = new CreateResumeViewModel { SearcherId = curUserId };
			return View(createResumeViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateResumeViewModel resumeVM)
		{
			if (ModelState.IsValid)
			{
				var resume = new Resume
				{
					Id = resumeVM.Id,
					SearcherId = resumeVM.SearcherId,
					Title = resumeVM.Title,
					Skills = resumeVM.Skills,
					Experience = resumeVM.Experience,
					Education = resumeVM.Education,
					ResumeStatus = resumeVM.ResumeStatus, //тут не впевнений що буде передаватсия 
					ContactInfo = resumeVM.ContactInfo

					// додати дату 
				};
				_resumeRepository.Add(resume);
				return RedirectToAction("Index","Dashboard");
			}
			else
			{
				ModelState.AddModelError("", "Помилка під час додавання резюме");
			}
			return View(resumeVM);
		}



		public async Task<IActionResult> Delete(int id)
		{
			var resumeDetails = await _resumeRepository.GetByIdAsync(id);
			if (resumeDetails == null)
				return View("Error");

			return View(resumeDetails);

		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteVacancy(int id)
		{
			var resumeDetails = await _resumeRepository.GetByIdAsync(id);
			if (resumeDetails == null)
				return View("Error");

			_resumeRepository.Delete(resumeDetails);
			return RedirectToAction("Index","Dashboard");

		}



		public async Task<IActionResult> Edit(int id)
		{
			var resume = await _resumeRepository.GetByIdAsyncNoTraking(id);
			if (resume == null)
				return View("Error");
			var resumeVM = new EditResumeViewModel
			{
				Id = resume.Id,
				SearcherId = resume.SearcherId,
				Title = resume.Title,
				Skills = resume.Skills,
				Experience = resume.Experience,
				Education = resume.Education,
				ResumeStatus = resume.ResumeStatus,
				ContactInfo = resume.ContactInfo,

			};
			return View(resumeVM);
		}
		[HttpPost]

		public async Task<IActionResult> Edit(int id, EditResumeViewModel resumeVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Помилка при редагуванні резюме.");
				return View("Edit", resumeVM);

			}
			else
			{	
				var userResume = await _resumeRepository.GetByIdAsyncNoTraking(id);
				if (userResume!= null)
				{
					var resume = new Resume
					{
						Id = id,
						Title = resumeVM.Title,
						SearcherId = resumeVM.SearcherId,
						Skills = resumeVM.Skills,
						Experience = resumeVM.Experience,
						Education= resumeVM.Education,
						ContactInfo= resumeVM.ContactInfo,
						ResumeStatus= Data.Enum.Status.Moderation,

					};
					if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
					{
						resume.ResumeStatus = resumeVM.ResumeStatus;
					}
					_resumeRepository.Update(resume);
					return RedirectToAction("Index", "Dashboard");

				}
				else
				{
					return View(resumeVM);
				}
			}
		}

	}
}
