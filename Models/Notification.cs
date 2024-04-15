using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Models
{
    public class Notification
    {
        [Key]
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
        [ForeignKey("User")]
        public string? OwnerId
        {
            get; set;
        }
        public User Owner
        {
            get; set;
        }
        [ForeignKey("Vacancy")]
        public int VacancyId
        {
            get; set;
        }
        public Vacancy Vacancy
        {
        get; set; }

    }
}
