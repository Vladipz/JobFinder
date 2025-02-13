﻿using JobFinder.Data.Enum;
using JobFinder.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.ViewModels
{
	public class CreateVacancyViewModel
	{
		[Key]
		public int Id
		{
			get; set;
		}
		[Required(ErrorMessage = "Назва є обов'язковим полем.")]
		public string Title
		{
			get; set;
		}
		public string? ShortDescription
		{
			get; set;
		}
		public string? LongDescription
		{
			get; set;
		}

		public string? Requirements
		{
			get; set;
		}
		[Required(ErrorMessage = "Заробітня плата є обов'язковим полем.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Заробітня плата повинна бути більше нуля.")]
		public decimal Wage
		{
			get; set;
		}
		public Status VacancyStatus
		{
			get; set;
		}

		[Required(ErrorMessage = "Поле 'Країна' є обов'язковим")]
		public string Country
		{
			get; set;
		}

		[Required(ErrorMessage = "Поле 'Місто' є обов'язковим")]
		public string City
		{
			get; set;
		}

		public JobType Type
		{
			get; set;
		}
		public DateTime? PostDate
		{
			get; set;
		}

	

	
		
		public string? EmployerId
		{
			get; set;
		}
		
	}
}
