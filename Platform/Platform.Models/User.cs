using Platform.Models.Interfaces;

namespace Platform.Models
{
	public class User : IPlatformModel
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}