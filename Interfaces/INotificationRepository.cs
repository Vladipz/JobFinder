using JobFinder.Models;

namespace JobFinder.Interfaces
{
    public interface INotificationRepository
    {

        Task<IEnumerable<Notification>> GetAllNotifications();
		List<Notification> GetAllUserNotifications();

		bool Add(Notification notification);
        bool Delete(Notification notification);
        bool Update(Notification notification);
        bool Save();
    }
}
