using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Repository
{
	public class InteriewRepository : IInterviewRepository
	{
		private readonly ApplicationDbContext _context;

		public InteriewRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Interview interview)
		{
			_context.Add(interview);
			return Save();
		}

		public bool Delete(Interview interview)
		{
			_context.Remove(interview);
			return Save();
		}

		public async Task<IEnumerable<Interview>> GetAll()
		{
			return await _context.Interviews.ToListAsync();
		}

		public Task<Interview> GetByIdAsync(int id)
		{
			return _context.Interviews.Include(i => i.Vacancy).ThenInclude(v => v.Employer).Include(i =>i.Searcher).FirstOrDefaultAsync(i => i.Id ==id);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Interview interview)
		{
			_context.Update(interview);
			return Save();
		}
	}
}
