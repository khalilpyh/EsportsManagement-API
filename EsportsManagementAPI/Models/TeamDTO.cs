/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EsportsManagementAPI.Models
{
	[ModelMetadataType(typeof(TeamMetaData))]
	public class TeamDTO: IValidatableObject
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public string Region { get; set; }

		public string Country { get; set; }

		public DateTime CreateDate { get; set; }

		public double TotalWinnings { get; set; }

		public int? NumberOfPlayers { get; set; } = null;

		public int GameID { get; set; }

		public GameDTO Game { get; set; }  //Game - Team (1:m)

		public ICollection<PlayerDTO> Players { get; set; }    //Team - Player (1:m)

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			if (CreateDate > DateTime.Today)   //establishment date cannot be in the future
			{
				yield return new ValidationResult("Create Date cannot be in the future.", new[] { "CreateDate" });
			}
		}
	}
}
