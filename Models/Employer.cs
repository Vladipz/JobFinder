namespace JobFinder.Models
{
    public class Employer : User
    {
        public List<Vacancy>? Vacancies{ get; set; } // список вакансій які створені цим користувачем
        
    }
}