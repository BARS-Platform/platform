using System.ComponentModel.DataAnnotations.Schema;
using Platform.Models.Interfaces;

namespace Platform.Models
{
//	[Table("users")]
	public class User : IPlatformModel
	{
//[]
		public long Id { get; set; }
//		[Column("login")]
		public string Login { get; set; }
//		[Column("email")]
		public string Email { get; set; }
//		[Column("password")]
		public string Password { get; set; }
	}
}