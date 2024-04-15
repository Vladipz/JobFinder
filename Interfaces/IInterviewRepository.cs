using JobFinder.Models;

namespace JobFinder.Interfaces
{
	public interface IInterviewRepository
	{
		Task<IEnumerable<Interview>> GetAll();
		Task<Interview> GetByIdAsync(int id);

		bool Add(Interview interview);
		bool Update(Interview interview);
		bool Delete(Interview interview);
		bool Save();
	}
}
