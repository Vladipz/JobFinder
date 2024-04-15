using Microsoft.AspNetCore.Identity;

namespace JobFinder.Models
{
    public class User : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        public string Name
        {
            get; set;
        }
        public string Surname
        {
            get; set;
        }
        //public string Email { get; set; }
        //public string Password { get; set; }


        public List<User>? Subscribers
        {
            get; set;
        }

        public List<User>? Subscriptions
        {
            get; set;
        }

        public List<Notification>? Notifications
        {
        get; set; }
    }
}
