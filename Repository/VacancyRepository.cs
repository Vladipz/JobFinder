using JobFinder.Data;
using JobFinder.Data.Enum;
using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JobFinder.Repository
{
	public class VacancyRepository : IVacancyRepository
	{
		private readonly ApplicationDbContext _context;

		public VacancyRepository(ApplicationDbContext context)
        {
			_context = context;
		}
        public bool Add(Vacancy vacancy)
		{
			vacancy.PostDate = DateTime.Now;
			_context.Add(vacancy);
			return Save();
		}

		public bool Delete(Vacancy vacancy)
		{
			_context.Remove(vacancy);
			return Save();
		}

		public async Task<IEnumerable<Vacancy>> GetAll()
		{
			return await _context.Vacancies.ToListAsync();
		}

		public async Task<IEnumerable<Vacancy>> GetAllBySearch(string SearchedText)
		{
			return await _context.Vacancies.Where(p => p.Title.Contains(SearchedText)).ToListAsync();
		}

		public async Task<IEnumerable<Vacancy>> GetAllBySearchAndStatus(string SearchedText, Status vacancyStatus)
		{
			return await _context.Vacancies.Where(v => v.VacancyStatus == vacancyStatus && v.Title.Contains(SearchedText)).ToListAsync();
		}

		public async Task<Vacancy> GetByIdAsync(int id)
		{
			return await _context.Vacancies.Include(i => i.Employer.Subscribers).FirstOrDefaultAsync(i => i.Id == id);
		}
		public Task<Vacancy> GetByIdAsyncNoTraking(int id)
		{
			return _context.Vacancies.Include(i => i.Employer).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		public async Task<IEnumerable<Vacancy>> GetByStatusAsync(Status vacancyStatus)
		{
			return await _context.Vacancies.Where(v=> v.VacancyStatus == vacancyStatus).ToListAsync();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved>0 ? true : false; 
		}

		public bool Update(Vacancy vacancy)
		{
			_context.Update(vacancy);
			return Save();
		}
	}
}
