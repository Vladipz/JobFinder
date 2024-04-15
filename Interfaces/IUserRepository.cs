using JobFinder.Models;

namespace JobFinder.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllUsers();
		Task<User> GetUserById(string id);

        Task<User> GetUserByName(string name);

        Task<User> GetUserByNameWithSubs(string name);

        bool Add(User user);
		bool Delete(User user);
		bool Update(User user);
		bool Save();

		Task<IEnumerable<User>> GetSubscribersByUserId(string userId);

        Task<IEnumerable<User>> GetSubscriptionsByUserId(string userId);

        Task<bool> AddSubscriber(string userId, string subscriberId);
        Task<bool> RemoveSubscriber(string userId, string subscriberId);



    }
}
