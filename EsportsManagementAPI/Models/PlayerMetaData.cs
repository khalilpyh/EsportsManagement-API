/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportsManagementAPI.Models
{
	public class PlayerMetaData : IValidatableObject
	{
		[Display(Name = "Player")]
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		[Display(Name = "First Name")]
		[Required(ErrorMessage = "You cannot leave the First Name blank.")]
		[StringLength(30, ErrorMessage = "First Name cannot be more than 30 characters long.")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		[Required(ErrorMessage = "You cannot leave the Last Name blank.")]
		[StringLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "You cannot leave the Nickname blank.")]
		[StringLength(50, ErrorMessage = "Nickname cannot be more than 50 characters long.")]
		public string Nickname { get; set; }

		[DataType(DataType.Date)]
		[Required(ErrorMessage = "You cannot leave the Date of Birth blank.")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DOB { get; set; }

		[Required(ErrorMessage = "You cannot leave the position blank.")]
		[StringLength(30, ErrorMessage = "Position cannot be more than 30 characters long.")]
		public string Position { get; set; }

		[Display(Name = "Join Date")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "You cannot leave the Join Date blank.")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime JoinDate { get; set; }

		//[Timestamp]
		//public Byte[] RowVersion { get; set; }  //diabled currently due to no identity in maui webapi client

		[Display(Name = "Team")]
		[Required(ErrorMessage = "You must select the Team.")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a Team.")]
		public int TeamID { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime today = DateTime.Today;
			int age = today.Year - DOB.Year - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));

			if (age < 16)   //player age cannot be below 16 years old
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
