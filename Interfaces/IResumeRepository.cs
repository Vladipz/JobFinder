using JobFinder.Models;

namespace JobFinder.Interfaces
{
	public interface IResumeRepository
	{
		Task<IEnumerable<Resume>> GetAll();
		Task<Resume> GetByIdAsync(int id);
		Task<Resume> GetByIdAsyncNoTraking(int id);


		bool Add(Resume resume);
		bool Update(Resume resume);
		bool Delete(Resume resume);
		bool Save();
	}
}
