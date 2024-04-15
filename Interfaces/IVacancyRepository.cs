using JobFinder.Data.Enum;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Interfaces
{
	public interface IVacancyRepository
	{
		Task<IEnumerable<Vacancy>> GetAll();
		Task<Vacancy> GetByIdAsync(int id);
		Task<Vacancy> GetByIdAsyncNoTraking(int id);

		Task<IEnumerable<Vacancy>> GetAllBySearch(string SearchedText);
		Task<IEnumerable<Vacancy>> GetAllBySearchAndStatus(string SearchedText, Status vacancyStatus);


		Task<IEnumerable<Vacancy>> GetByStatusAsync(Status vacancyStatus); 

		bool Add(Vacancy vacancy);
		bool Update(Vacancy vacancy);
		bool Delete(Vacancy vacancy);
		bool Save();



	}
}
