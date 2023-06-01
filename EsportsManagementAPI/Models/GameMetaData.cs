/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace EsportsManagementAPI.Models
{
	public class GameMetaData : IValidatableObject
	{
		[Display(Name = "Game Name")]
		[Required(ErrorMessage = "You cannot leave the Game Name blank.")]
		[StringLength(100, ErrorMessage = "Game Name cannot be more than 100 characters long.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "You cannot leave the Developer blank.")]
		[StringLength(100, ErrorMessage = "Developer cannot be more than 100 characters long.")]
		public string Developer { get; set; }

		[Required(ErrorMessage = "You cannot leave the Publisher blank.")]
		[StringLength(100, ErrorMessage = "Publisher cannot be more than 100 characters long.")]
		public string Publisher { get; set; }

		[Required(ErrorMessage = "You cannot leave the Designer blank.")]
		[StringLength(100, ErrorMessage = "Designer cannot be more than 100 characters long.")]
		public string Designer { get; set; }

		[Required(ErrorMessage = "You cannot leave the Engine blank.")]
		[StringLength(100, ErrorMessage = "Engine cannot be more than 100 characters long.")]
		public string Engine { get; set; }

		[Display(Name = "Release Date")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "You cannot leave the Release Date blank.")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReleaseDate { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ReleaseDate > DateTime.Today)   //game release date cannot be in the future
			{
				yield return new ValidationResult("Create Date cannot be in the future.", new[] { "ReleaseDate" });
			}
		}
	}
}
