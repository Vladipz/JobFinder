using JobFinder.Data.Enum;
using JobFinder.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;

namespace JobFinder.Data
{
	public class Seed
	{
		public static async Task SeedDataAsyncresume100000(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				string SearcherEmail = "emily.davis@email.com";

				var Searcher = await userManager.FindByEmailAsync(SearcherEmail);
				for (int i = 0; i < 100000; i++)
				{
					var resume = new Resume()
					{
						Title = $"Test_{i}",
						ContactInfo = $"Test_{i}",
						Education = $"Test_{i}",
						Experience  = $"Test_{i}",
						Searcher = (Searcher)Searcher,
						Skills = $"Test_{i}"
					};
					context.Resumes.Add(resume);
					if (i % 100 == 0)
					{
						await context.SaveChangesAsync();
					}
				}

			}
		}

		public static async Task SeedDataAsync100000(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				string EmployerEmail = "sophia.brown@email.com";

				var Employer = await userManager.FindByEmailAsync(EmployerEmail);
				for (int i = 0; i < 75000; i++)
				{
					var vacancy = new Vacancy()
					{
						Title = $"Test_{i}",
						ShortDescription = "We are looking for a skilled front-end developer to join our team.",
						LongDescription = "As a front-end developer, you will be responsible for creating and maintaining the user interface of our web applications. You will work closely with the design team to implement responsive and user-friendly interfaces.",
						Requirements = "Experience with HTML, CSS, JavaScript.",
						Wage = 60000,
						VacancyStatus = Status.Approved,
						Employer = (Employer)Employer,
						Country = "Ukraine",
						City = "Lviv",
						PostDate = DateTime.Now,
						Type = JobType.Online
					};
					context.Vacancies.Add(vacancy);
					if (i % 100 == 0)
					{
						await context.SaveChangesAsync();
					}
				}
				
			}

		}

		public static async Task SeedDataAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
				string EmployerEmail = "david.garcia@example.com";

				var Employer = await userManager.FindByEmailAsync(EmployerEmail);
				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				context.Database.EnsureCreated();
				
				if (!context.Vacancies.Any())
				{
					context.Vacancies.AddRange(new List<Vacancy>()
					{
						 new Vacancy()
						 {
								Title = "Front-end Developer",
								ShortDescription = "We are looking for a skilled front-end developer to join our team.",
								LongDescription = "As a front-end developer, you will be responsible for creating and maintaining the user interface of our web applications. You will work closely with the design team to implement responsive and user-friendly interfaces.",
								Requirements = "Experience with HTML, CSS, JavaScript.",
								Wage = 60000,
								VacancyStatus = Status.Approved,
								Employer = (Employer)Employer,
								Country = "Ukraine",
								City = "Lviv",
								PostDate = DateTime.Now,
								Type = JobType.Online
						 },
						new Vacancy()
						{
							Title = "Backend Developer",
							ShortDescription = "We need a backend developer to work on server-side applications.",
							LongDescription = "The backend developer will be responsible for developing server-side logic, working with databases, and ensuring the performance and responsiveness of our applications. Proficiency in Java, Python, or C# is required.",
							Requirements = "Proficiency in Java, Python, or C#.",
							Wage = 65000,
							VacancyStatus = Status.Approved,
							Employer = (Employer)Employer,
								Country = "Ukraine",
								City = "Kyiv",
								PostDate = DateTime.Now,
								Type = JobType.Online

							},
						new Vacancy()
						{
							Title = "Data Analyst",
							ShortDescription = "Seeking a data analyst to analyze and interpret data for insights.",
							LongDescription = "The data analyst will be responsible for collecting and analyzing data to identify trends, generate reports, and provide valuable insights. Strong analytical and problem-solving skills are required.",
							Wage = 55000,
							VacancyStatus = Status.Moderation,
							Employer = (Employer)Employer,
								Country = "Ukraine",
								City = "Kharkiv",
								PostDate = DateTime.Now,
								Type = JobType.Offline
							},
						new Vacancy()
						{

							Title = "Front-end Developer",
							ShortDescription = "We are looking for a skilled front-end developer to join our team.",
							LongDescription = "As a front-end developer, you will be responsible for creating and maintaining the user interface of our web applications. You will work closely with the design team to implement responsive and user-friendly interfaces.",
							Requirements = "Experience with HTML, CSS, JavaScript.",
							Wage = 60000,
							VacancyStatus = Status.Approved,
							Employer = (Employer)Employer,
								Country = "Ukraine",
								City = "Zhytomyr",
								PostDate = DateTime.Now,
								Type = JobType.Offline
							},
					});
					context.SaveChanges();
				}

			}
		}

		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				//Roles
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
				if (!await roleManager.RoleExistsAsync(UserRoles.Employer))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Employer));
				if (!await roleManager.RoleExistsAsync(UserRoles.Searcher))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Searcher));

				//Users
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
				string adminUserEmail = "john.smith@example.com";

				var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				if (adminUser == null)
				{
					var newAdminUser = new User()
					{
						UserName = adminUserEmail,
						Name = "John",
						Surname = "Smith",
						Email = adminUserEmail,
						EmailConfirmed = true,
						
					};
					await userManager.CreateAsync(newAdminUser, "password2");
					await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				}

				string appUserEmail = "jane.jones@example.com";

				var searcher = await userManager.FindByEmailAsync(appUserEmail);
				if (searcher == null)
				{
					var newAppUser = new User()
					{
						UserName = appUserEmail,
						Name = "Jane",
						Surname = "Jones",
						Email = appUserEmail,
						EmailConfirmed = true,

					};
					await userManager.CreateAsync(newAppUser, "password1");
					await userManager.AddToRoleAsync(newAppUser, UserRoles.Searcher);
				}


				string EmployerEmail =  "david.garcia@example.com";
				var Employer = await userManager.FindByEmailAsync(EmployerEmail);
				if (Employer == null)
				{
					var newAppUser = new Employer()
					{
						UserName = EmployerEmail,
						Name = "David",
						Surname = "Garcia",
						Email = EmployerEmail,
						EmailConfirmed = true,

					};
					await userManager.CreateAsync(newAppUser, "password12");
					await userManager.AddToRoleAsync(newAppUser, UserRoles.Employer);
				}

				string EmployerEmail2 = "michael.lopez@example.com";
				var Employer2 = await userManager.FindByEmailAsync(EmployerEmail);
				if (Employer == null)
				{
					var newAppUser = new Employer()
					{
						UserName = EmployerEmail,
						Name = "Michael",
						Surname = "Lopez",
						Email = EmployerEmail,
						EmailConfirmed = true,

					};
					await userManager.CreateAsync(newAppUser, "password12");
					await userManager.AddToRoleAsync(newAppUser, UserRoles.Employer);
				}
			}
		}
	}
}
