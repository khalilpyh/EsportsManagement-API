/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportsManagementAPI.Models
{
	public class TeamMetaData: IValidatableObject
	{
		[Display(Name = "Team Name")]
		[Required(ErrorMessage = "You cannot leave the Team Name blank.")]
		[StringLength(50, ErrorMessage = "Team Name cannot be more than 50 characters long.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "You cannot leave the Region blank.")]
		[StringLength(30, ErrorMessage = "Region cannot be more than 30 characters long.")]
		public string Region { get; set; }

		[Required(ErrorMessage = "You cannot leave the Country blank.")]
		[StringLength(30, ErrorMessage = "Country cannot be more than 30 characters long.")]
		public string Country { get; set; }

		[Display(Name = "Create Date")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "You cannot leave the Create Date blank.")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime CreateDate { get; set; }

		[Required(ErrorMessage = "You cannot leave the Total Winnings blank.")]
		[Range(0.0, Double.MaxValue, ErrorMessage = "Total Winnings must be no less than $0.")]
		[DataType(DataType.Currency)]
		public double TotalWinnings { get; set; }

		[Display(Name = "Game")]
		[Required(ErrorMessage = "You must select the Game.")]
		[Range(1, int.MaxValue, ErrorMessage = "You must select a Game.")]
		public int GameID { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			if (CreateDate > DateTime.Today)   //establishment date cannot be in the future
			{
				yield return new ValidationResult("Create Date cannot be in the future.", new[] { "CreateDate" });
			}
		}
	}
}
