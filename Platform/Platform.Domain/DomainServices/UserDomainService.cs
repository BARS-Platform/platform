using System.IdentityModel.Tokens.Jwt;
using Platform.Domain.Common;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Services;

namespace Platform.Domain.DomainServices
{
	public class UserDomainService
	{
		private readonly IRepository _repository;
		private readonly PasswordCheckerService _checkerService;
		private readonly TokenService _tokenService;

		public UserDomainService(IRepository repository,
			PasswordCheckerService checkerService, TokenService tokenService)
		{
			_repository = repository;
			_checkerService = checkerService;
			_tokenService = tokenService;
		}

		public OperationResult CheckLoginUsed(string login)
		{
			var user = _repository.FindByPredicate<User>(x => x.Login == login);

			return new OperationResult(user == null);
		}

		public OperationResult CheckEmailUsed(string email)
		{
			var user = _repository.FindByPredicate<User>(x => x.Email == email);

			return new OperationResult(user == null);
		}

		public OperationResult LogIn(string login, string password)
		{
			var user = _repository.FindByPredicate<User>(x => x.Login == login)
			           ?? throw new AuthorizationException(
				           "User with such login does not exist. Please review your credentials.", nameof(login));

			if (!_checkerService.Check(user.Password, password))
			{
				throw new AuthorizationException("Invalid password. Please review your credentials.", nameof(password));
			}

			return new OperationResult()
			{
				Success = true,
				Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(user))
			};
		}

		public OperationResult Register(string login, string password, string email)
		{
			var user = new User(login, _checkerService.HashPassword(password), email);

			_repository.Create(user);

			return new OperationResult()
			{
				Success = true,
				Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(user))
			};
		}
	}
}