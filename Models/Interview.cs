using JobFinder.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Models
{
    public class Interview
    {
        [Key]
        public int Id{get; set;}
        public DateTime InterviewDate{get; set;}
        public TimeSpan InterviewTime{get; set;}
        public string Platform{get; set;}
        [ForeignKey("Searcher")]
        public string? SearcherId
		{get; set;} // Зовнішній ключ
        public Searcher? Searcher{get; set;}
        public Vacancy? Vacancy{get; set;}
        public InterviewStatus Status {get; set;}



	}
}