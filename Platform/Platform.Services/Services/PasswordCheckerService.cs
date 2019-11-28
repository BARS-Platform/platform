using System;
using System.Linq;
using System.Security.Cryptography;

namespace Platform.Services.Services
{
	/// <summary>
	/// Service that validates user's password.
	/// </summary>
	public class PasswordCheckerService
	{
		private const int SaltSize = 16;
		private const int KeySize = 32;
		private const int Iterations = 10000;

		/// <summary>
		/// Get password's hash by SHA256 algorithm.
		/// </summary>
		public string HashPassword(string password)
		{
			using var algorithm = new Rfc2898DeriveBytes(
				password,
				SaltSize,
				Iterations,
				HashAlgorithmName.SHA256);
			var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
			var salt = Convert.ToBase64String(algorithm.Salt);

			return $"{salt}.{key}";
		}

		/// <summary>
		/// Validate received password to stored hash.
		/// </summary>
		/// <param name="hash">Hash, stored at user record in database.</param>
		/// <param name="password">Received input</param>
		public bool Check(string hash, string password)
		{
			var parts = hash.Split('.', 2);

			if (parts.Length != 2)
			{
				throw new FormatException("Unexpected hash format. " +
				                          "Should be formatted as `{salt}.{hash}`");
			}

			var salt = Convert.FromBase64String(parts[0]);
			var key = Convert.FromBase64String(parts[1]);

			using var algorithm = new Rfc2898DeriveBytes(
				password,
				salt,
				Iterations,
				HashAlgorithmName.SHA256);
			var keyToCheck = algorithm.GetBytes(KeySize);

			return keyToCheck.SequenceEqual(key);
		}
	}
}