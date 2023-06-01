﻿/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System.ComponentModel.DataAnnotations;

namespace EsportsManagementAPI.Models
{
	public abstract class Auditable : IAuditable
	{
		[ScaffoldColumn(false)]
		[StringLength(256)]
		public string CreatedBy { get; set; }

		[ScaffoldColumn(false)]
		public DateTime? CreatedOn { get; set; }

		[ScaffoldColumn(false)]
		[StringLength(256)]
		public string UpdatedBy { get; set; }

		[ScaffoldColumn(false)]
		public DateTime? UpdatedOn { get; set; }

	}
}
