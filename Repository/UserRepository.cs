using CloudinaryDotNet.Actions;
using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace JobFinder.Repository
{
    public class UserRepository : IUserRepository/*, IObservable<User>*/
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
            //return await _context.Searchers.Include(s=>s.SavedVacancies).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<User> GetUserByName(string name)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == name);

            return user;
        }
        public Task<User> GetUserByNameWithSubs(string name)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetSubscribersByUserId(string userId)
        {
            var user = await _context.Users
            .Include(u => u.Subscribers) // Include the Subscribers navigation property
            .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Subscribers ?? Enumerable.Empty<User>();
        }

        public async Task<IEnumerable<User>> GetSubscriptionsByUserId(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Subscriptions?? Enumerable.Empty<User>();


        }

        public async Task<bool> AddSubscriber(string userId, string subscriberId)
        {
            var user = await _context.Users.FindAsync(userId);
            var subscriber = await _context.Users.FindAsync(subscriberId);

            if (user != null && subscriber != null)
            {
                user.Subscribers ??= new List<User>();
                user.Subscribers.Add(subscriber);

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveSubscriber(string userId, string subscriberId)
        {
            var user = await _context.Users
                .Include(u => u.Subscribers)
                .FirstOrDefaultAsync(u => u.Id == userId);
            var subscriber = await _context.Users.FindAsync(subscriberId);

            if (user != null && subscriber != null)
            {
                user.Subscribers?.Remove(subscriber);

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        
    }
}
