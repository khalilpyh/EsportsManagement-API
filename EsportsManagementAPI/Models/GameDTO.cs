/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace EsportsManagementAPI.Models
{
	[ModelMetadataType(typeof(GameMetaData))]
	public class GameDTO : IValidatableObject
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public string Developer { get; set; }

		public string Publisher { get; set; }

		public string Designer { get; set; }

		public string Engine { get; set; }

		public DateTime ReleaseDate { get; set; }

		public ICollection<TeamDTO> Teams { get; set; }  //Game - Team (1 : m)

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ReleaseDate > DateTime.Today)   //game release date cannot be in the future
			{
				yield return new ValidationResult("Create Date cannot be in the future.", new[] { "ReleaseDate" });
			}
		}
	}
}
