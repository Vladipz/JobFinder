using JobFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Searcher> Searchers{get; set;}
        public DbSet<Employer> Employers{get; set;}
        public DbSet<Vacancy> Vacancies{get; set;}
        public DbSet<Interview> Interviews{get; set;}
        public DbSet<Resume> Resumes{ get; set;}
        public DbSet<Notification> Notifications
        { get; set;}


		//public DbSet<Searcher> SavedSearchers
		//{
		//    get; set;
		//}
		//public DbSet<Vacancy> SavedVacancies
		//{
		//    get; set;
		//}
		//public DbSet<Vacancy> JobApplications
		//{
		//    get; set;
		//}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Searcher>()
                    .HasMany(s => s.JobApplications) // у кожного шукача є багато поаних заявок
                    .WithMany(v => v.Searchers) // у кожної вакансії є багато шукачів
                    .UsingEntity(j => j.ToTable("UserJobApplication"));
                    
            modelBuilder.Entity<Searcher>()
                    .HasMany(s => s.SavedVacancies) // у кожного шукача є багато збережених вакасій
                    .WithMany(v => v.SavedSearchers) // кожну вакансії можуть зберігати багато користувачів
                    .UsingEntity(j => j.ToTable("SavedVacancy"));

            modelBuilder.Entity<Vacancy>()
                .Property(v => v.Wage)
                .HasColumnType("decimal(18, 2)");
			modelBuilder.Entity<Interview>()
				.HasOne(i => i.Searcher)
				.WithMany(s => s.Interviews)
				.HasForeignKey(i => i.SearcherId);

            // Конфігурація зв'язку багато-до-багатьох
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subscribers)
                .WithMany(u => u.Subscriptions)
                .UsingEntity(j => j.ToTable("UserSubscriptions"));





            //modelBuilder.Entity<User>().HasKey(u => u.Id);
            base.OnModelCreating(modelBuilder);



			//modelBuilder.Entity<Interview>()
			//    .HasOne(i => i.Vacancy)
			//    .WithMany()
			//    //.HasForeignKey(i => i.VacancyId)
			//    .OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<Interview>()
			//    .HasOne(i => i.Searcher)
			//    .WithMany()
			//    //.HasForeignKey(i => i.SearcherId)
			//    .OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<Interview>()
			//    .HasOne(i => i.Employer)
			//    .WithMany()
			//    //.HasForeignKey(i => i.EmployerId)
			//    .OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<Interview>()
			//    .HasOne(i => i.Employer)
			//    .WithMany(e => e.Interviews)
			//    .HasForeignKey(i => i.EmployerId);




		}


	}
}
