namespace JobFinder.Models
{
    public class Searcher : User
    {
        public List<Vacancy>? JobApplications // подані зявки
        {
            get; set;
        }

        public List<Vacancy>? SavedVacancies // збережені зявки
        {
            get; set;
        }
      
        public List<Interview>? Interviews
        {
            get; set;
        }
        //додати список резюме
        public List<Resume>? Resumes
        {
            get; set;
        }
    }
}