using JobFinder.Models;

namespace JobFinder.Interfaces
{
	public interface IDashboardRepository
	{
		Task<List<Vacancy>> GetAllUserVacancy();
		Task<List<Resume>> GetAllUserResumes();


	}
}
