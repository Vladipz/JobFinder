using JobFinder.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.ViewModels
{
	public class EditResumeViewModel
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

		[Required(ErrorMessage = "Name is a required field.")]
		public string Title
		{
			get; set;
		}
		[Required(ErrorMessage = "Skills is a required field.")]

		public string Skills
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

		public Status ResumeStatus
		{
			get; set;
		}
		[Required(ErrorMessage = "Contact information is a required field.")]
		public string? ContactInfo
		{
			get; set;
		}

	}
}
