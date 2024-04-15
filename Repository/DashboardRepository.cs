using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.Models;
using System.Linq;

namespace JobFinder.Repository
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContext;

		public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
			_context = context;
			_httpContext = httpContext;
		}

		public async Task<List<Resume>> GetAllUserResumes()
		{
			var curUser = _httpContext.HttpContext?.User.GetUserId();
			var userResumes = _context.Resumes.Where(r => r.SearcherId == curUser);
			return userResumes.ToList();
		}

		public async Task<List<Vacancy>> GetAllUserVacancy()
		{
			var curUser = _httpContext.HttpContext?.User.GetUserId();
			var userVacancy =  _context.Vacancies.Where(v => v.EmployerId == curUser);
			return userVacancy.ToList();
		}
	}
}
