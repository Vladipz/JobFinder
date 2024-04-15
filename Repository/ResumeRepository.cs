using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Repository
{
	public class ResumeRepository : IResumeRepository
	{
		private readonly ApplicationDbContext _context;

		public ResumeRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public bool Add(Resume resume)
		{
			_context.Add(resume);
			return Save();
		}

		public bool Delete(Resume resume)
		{
			_context.Remove(resume);
			return Save();
		}

		public async Task<IEnumerable<Resume>> GetAll()
		{
			return await _context.Resumes.ToListAsync();
		}

		public Task<Resume> GetByIdAsync(int id)
		{
			return _context.Resumes.Include(r => r.Searcher).FirstOrDefaultAsync(i => i.Id == id);
		}
		public Task<Resume> GetByIdAsyncNoTraking(int id)
		{
			return _context.Resumes.Include(r => r.Searcher).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Resume resume)
		{
			_context.Update(resume);
			return Save();
		}
	}
}
