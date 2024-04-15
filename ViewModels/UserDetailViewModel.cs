namespace JobFinder.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        public string Email{ get; set; }
        public bool IsSubscribed
        {
            get; set;
        }

    }
}
