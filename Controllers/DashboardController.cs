using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardRepository _dashboardRepository;

		public DashboardController(IDashboardRepository dashboardRepository)
        {
			_dashboardRepository = dashboardRepository;
		}
        public async Task<IActionResult> Index()
		{
			var userVacancy = await _dashboardRepository.GetAllUserVacancy();
			var userResumes = await _dashboardRepository.GetAllUserResumes();

			var dashboardVM = new DashboardViewModel
			{
				Vacancies = userVacancy,
				Resumes = userResumes,
			};
			return View(dashboardVM);
		}
	}
}
