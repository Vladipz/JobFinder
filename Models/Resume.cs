using JobFinder.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace JobFinder.Models
{
    public class Resume
    {
        [Key]
        public int Id
        {
            get; set;
        }
        
        [ForeignKey("Searcher")]
        public string? SearcherId
        {
            get; set;
        }
        public Searcher Searcher
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }
        public string? Skills
        {
            get; set;
        }
        public string? Experience
        {
            get; set;
        }
        public string? Education
        {
            get; set;
        }

        public Status ResumeStatus { get; set; }

        public string? ContactInfo { get; set; }

    }
}