using JobFinder.Data;
using JobFinder.Interfaces;
using JobFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContext;

		public NotificationRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
			_httpContext = httpContext;
		}
        public bool Add(Notification notification)
        {
            Console.WriteLine("Add 5");
            try
            {
                _context.Add(notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Add: {ex.Message}");
                return false;
            }
            
            Console.WriteLine("Add 6");

            return Save();
        }

        public bool Delete(Notification notification)
        {
            _context.Remove(notification);
            return Save();
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
						throw new NotImplementedException();


		}

		public List<Notification> GetAllUserNotifications()
		{
            var curUser = _httpContext.HttpContext?.User.GetUserId();
            var userNotification = _context.Notifications.Where(v => v.OwnerId == curUser);
            return  userNotification.ToList();
        }

		public bool Save()
        {
            try
            {
                Console.WriteLine("Save 6");
                var saved = _context.SaveChanges();
                Console.WriteLine("Save 7");

                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                // Обробка помилок, виведення або реєстрація інформації про помилку
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                return false;
            }
        }

        public bool Update(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
