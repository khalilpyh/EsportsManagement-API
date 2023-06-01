/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

namespace EsportsManagementAPI.Models
{
	internal interface IAuditable
	{
		string CreatedBy { get; set; }
		DateTime? CreatedOn { get; set; }
		string UpdatedBy { get; set; }
		DateTime? UpdatedOn { get; set; }
	}
}
