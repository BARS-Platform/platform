using System.IdentityModel.Tokens.Jwt;
using Platform.Database;
using Platform.Domain.Common;
using Platform.Domain.Services;
using Platform.Models;

namespace Platform.Domain.DomainServices
{
    public class UserDomainService
    {
        private readonly IRepository<User> _repository;
        private readonly PasswordCheckerService _checkerService;
        private readonly TokenService _tokenService;

        public UserDomainService(IRepository<User> repository,
            PasswordCheckerService checkerService, TokenService tokenService)
        {
            _repository = repository;
            _checkerService = checkerService;
            _tokenService = tokenService;
        }

        public OperationResult LogIn(string login, string password)
        {
            try
            {
                var user = _repository.FindByPredicate(x => x.Login == login)
                    ?? throw new AuthorizationException("Invalid login. Please check your credentials.");
                
                if(!_checkerService.Check(user.Password, password))
                {
                    throw new AuthorizationException("Invalid password.Please check your credentials.");
                }
            }
            catch (AuthorizationException exception)
            {
                return new OperationResult(false, exception.Message);
            }

            return new OperationResult()
            {
                Success = true,
                Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(login))

            };
        }

        public OperationResult Register(string login, string password, string email)
        {
            _repository.Create(new User(login, _checkerService.HashPassword(password), email));
            
            return new OperationResult()
            {
                Success = true,
                Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(login))
            };
        }
    }
}
