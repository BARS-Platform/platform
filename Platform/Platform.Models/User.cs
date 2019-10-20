using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.Models
{
	[Table("users")]
	public class User
	{
		[Column("login")]
		public string Login { get; set; }
		[Column("email")]
		public string Email { get; set; }
		[Column("password")]
		public string Password { get; set; }
	}
}