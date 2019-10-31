using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Platform.Domain.Common;
using Platform.Domain.Services;
using Platform.Fatabase;
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
        
        public async Task<OperationResult> CheckLoginUsed(string login)
        {
            var user = await _repository.FindByPredicate(x => x.Login == login);

            return new OperationResult(user == null);
        }

        public async Task<OperationResult> CheckEmailUsed(string email)
        {
            var user = await _repository.FindByPredicate(x => x.Email == email);

            return new OperationResult(user == null);
        }

        public async Task<OperationResult> LogIn(string login, string password)
        {
            var user = await _repository.FindByPredicate(x => x.Login == login)
                       ?? throw new AuthorizationException("Invalid login. Please check your credentials.");

            if (!_checkerService.Check(user.Password, password))
            {
                throw new AuthorizationException("Invalid password.Please check your credentials.");
            }


            return new OperationResult()
            {
                Success = true,
                Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(user))
            };
        }

        public async Task<OperationResult> Register(string login, string password, string email)
        {
            var user = new User(login, _checkerService.HashPassword(password), email);

            await _repository.Create(user);
            
            return new OperationResult()
            {
                Success = true,
                Data = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(user))
            };
        }
    }
}
