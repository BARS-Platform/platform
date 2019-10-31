using System;
using Platform.Models.Interfaces;

namespace Platform.Models
{
	public class User : IPlatformModel
	{
		public User(string login, string password, string email)
		{
			UpdateLogin(login);
			UpdatePassword(password);
			UpdateEmail(email);
		}

		// EF Core support
		private User()
		{
		}

		public long Id { get; private set; }
		public string Login { get; private set; }
		public string Email { get; private set; }
		public string Password { get; private set; }

		public void UpdateLogin(string login)
		{
			if (string.IsNullOrEmpty(login))
				throw new ArgumentException("Parameter must be set.", nameof(login));

			Login = login;
		}

		public void UpdatePassword(string password)
		{
			if (string.IsNullOrEmpty(password))
				throw new ArgumentException("Parameter must be set.", nameof(password));

			if (password.IndexOf('.') == -1)
				throw new ArgumentException("Invalid password format. Ensure that you have hashed this password");

			Password = password;
		}

		public void UpdateEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				throw new ArgumentException("Parameter must be set.", nameof(email));

			if (email.IndexOf('@') == -1 || email.IndexOf('.') == -1)
				throw new ArgumentException($"Parameter {nameof(email)} must be of valid format: ***@***.**");

			Email = email;
		}
	}
}