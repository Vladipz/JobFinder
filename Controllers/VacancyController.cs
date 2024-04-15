using JobFinder.Data;
using JobFinder.Data.Enum;
using JobFinder.Interfaces;
using JobFinder.Models;
using JobFinder.Repository;
using JobFinder.Services;
using JobFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;

namespace JobFinder.Controllers
{
	public class VacancyController : Controller
	{
		private readonly IVacancyRepository _vacancyRepository;
		private readonly IVacancyService _vacancySercive;

        private readonly IHttpContextAccessor _httpContextAccessor;
		public VacancyController(IVacancyRepository vacancyRepository, IVacancyService vacancySercive, IHttpContextAccessor httpContextAccessor, NotificationsObserver notificationsObserver)
		{
			_vacancyRepository = vacancyRepository;
            _vacancySercive = vacancySercive;

            _httpContextAccessor = httpContextAccessor;


			// Створення екземпляру NotificationsObserver і підписка на VacancyService
			//var _notificationsObserver = notificationsObserver;
            _vacancySercive.Subscribe(notificationsObserver);




        }
		public async Task<IActionResult> Index(string SearchedText = "")
		{
			

			if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
			{
				if (SearchedText != "" && SearchedText != null)
				{
					var vacancies = await _vacancyRepository.GetAllBySearch(SearchedText);
					return View(vacancies);

				}
				else
				{
					var vacancies = await _vacancyRepository.GetAll();
					return View(vacancies);
				}

					
			}
			else
			{
				if (SearchedText != "" && SearchedText != null)
				{
					var vacancies = await _vacancyRepository.GetAllBySearchAndStatus(SearchedText, Status.Approved);
					return View(vacancies);

				}
				else
				{
					var vacancies = await _vacancyRepository.GetByStatusAsync(Status.Approved);
					return View(vacancies);
				}
	
			}
			
		}


		public async Task<IActionResult> Detail(int id)
		{
			Vacancy vacancy = await _vacancyRepository.GetByIdAsync(id);
			return View(vacancy);
		}
		public IActionResult Create()
		{
			var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			var createVacancyViewModel = new CreateVacancyViewModel { EmployerId = curUserId };
			return View(createVacancyViewModel);
		}
		
		
		
		[HttpPost]
		public async Task<IActionResult> Create(CreateVacancyViewModel vacancyVM)
		{
			
			if (ModelState.IsValid)
			{

				var vacancy = new Vacancy
				{
					Id = vacancyVM.Id,
					Title = vacancyVM.Title,
					ShortDescription = vacancyVM.ShortDescription,
					LongDescription = vacancyVM.LongDescription,
					Requirements = vacancyVM.Requirements,
					Wage = vacancyVM.Wage,
					VacancyStatus = vacancyVM.VacancyStatus,
					Country = vacancyVM.Country,
					City = vacancyVM.City,
					Type = vacancyVM.Type,
					PostDate = DateTime.Now,
					EmployerId = vacancyVM.EmployerId, // Замінити // замінив
					
				};
				_vacancyRepository.Add(vacancy);
				return RedirectToAction("Index");

				//return View(vacancy);
			}
			else
			{
				ModelState.AddModelError("", "Помилка під час додавання нової вакансії");
			}
			return View(vacancyVM);
			
		}
		public async Task<IActionResult> Edit(int id)
		{
			var vacancy = await _vacancyRepository.GetByIdAsync(id);
			if (vacancy == null) return View("Error");
			var vacancyVM = new EditVacancyViewModel
			{
				Id = vacancy.Id,
				Title = vacancy.Title,
				ShortDescription = vacancy.ShortDescription,
				LongDescription = vacancy.LongDescription,
				Requirements = vacancy.Requirements,
				Wage = vacancy.Wage,
				VacancyStatus = vacancy.VacancyStatus,
				Country = vacancy.Country,
				City = vacancy.City,
				Type = vacancy.Type,
				PostDate = DateTime.Now,
				EmployerId = vacancy.EmployerId,
				Employer = vacancy.Employer
			}; 
			return View(vacancyVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditVacancyViewModel vacancyVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Помилка при редагуванні вакансії."); 
				return View("Edit",vacancyVM);

			}
			var userVacancy = await _vacancyRepository.GetByIdAsyncNoTraking(id);
			if (userVacancy != null)
			{
				var vacancy = new Vacancy
				{
					Id = id,
					Title = vacancyVM.Title,
					ShortDescription = vacancyVM.ShortDescription,
					LongDescription = vacancyVM.LongDescription,
					Requirements = vacancyVM.Requirements,
					Wage = vacancyVM.Wage,
					VacancyStatus = Status.Moderation,
					Country = vacancyVM.Country,
					City = vacancyVM.City,
					Type = vacancyVM.Type, // можлива логічна помилка бо не присваюєтсья деяки поля і стають нул 
					PostDate = DateTime.Now,
					Employer = vacancyVM.Employer,
					EmployerId = vacancyVM.EmployerId,
					SavedSearchers = vacancyVM.SavedSearchers,
					Searchers = vacancyVM.Searchers,
					Interviews = vacancyVM.Interviews,

					
				};
				if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
				{
					vacancy.VacancyStatus = vacancyVM.VacancyStatus;
				}

				await _vacancySercive.Update(vacancy);
				//_vacancyRepository.Update(vacancy);


				return RedirectToAction("Index");
			}
			else
			{
				return View(vacancyVM);
			}
		
		}

		public async Task<IActionResult> Delete(int id)
		{
			var vacancyDetails = await _vacancyRepository.GetByIdAsync(id);
			if (vacancyDetails == null) return View("Error");
			
			return View(vacancyDetails);

		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteVacancy(int id)
		{
			var vacancyDetails = await _vacancyRepository.GetByIdAsync(id);
			if (vacancyDetails == null) return View("Error");

			_vacancyRepository.Delete(vacancyDetails);
			return RedirectToAction("Index");
		
		}

	}
}
