/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportsManagementAPI.Models
{
	[ModelMetadataType(typeof(PlayerMetaData))]
	public class Player : Auditable, IValidatableObject
	{
		public int ID { get; set; }

		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		public int Age
		{
			get
			{
				DateTime today = DateTime.Today;
				int age = today.Year - DOB.Year - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
				return age;
			}
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Nickname { get; set; }

		public DateTime DOB { get; set; }

		public string Position { get; set; }

		public DateTime JoinDate { get; set; }

		//public Byte[] RowVersion { get; set; }	//diabled currently due to no identity in maui webapi client

		public int TeamID { get; set; }

		public Team Team { get; set; }	//Team - Player (1:m)

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Age < 16)   //player age cannot be below 16 years old
			{
				yield return new ValidationResult("No one under 16 years old is allowed to resigster as a E-sports Player.", new[] { "DOB" });
			}
			if (DOB > DateTime.Today)	//birth date cannot be in the future
			{
				yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
			}
			if (DOB > JoinDate)	//unable to join a team before birth
			{
				yield return new ValidationResult("Join Date cannot be before Date of Birth.", new[] { "JoinDate" });
			}
			if (JoinDate > DateTime.Today)   //join date cannot be in the future
			{
				yield return new ValidationResult("Join Date cannot be in the future.", new[] { "JoinDate" });
			}
		}
	}
}
