using System.Linq;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Dto;

namespace Platform.Services.Services
{
    public class RoleService
    {
        private readonly IRepository _repository;

        public RoleService(IRepository repository)
        {
            _repository = repository;
        }

        public OperationResult GetAll(ListParam listParam)
        {
            var result = _repository
                .GetAll<Role>()
                .Select(RoleDto.ProjectionExpression)
                .FormData(listParam);
            
            return result == null
                ? new OperationResult(false, "No roles found")
                : new OperationResult(true, result);
        }
        
        public OperationResult AddRoleToUser(int userId, int roleId)
        {
            var user = _repository.Get<User>(userId);
            var role = _repository.Get<Role>(roleId);
            if (user == null)
                return new OperationResult(false, "User not found");
            if (role == null)
                return new OperationResult(false, "Role not found");

            var result = _repository.Create(new UserRole(user, role));
            
            return result == null
                ? new OperationResult(false, "Create error")
                : new OperationResult(true, result);
        }
        
        public OperationResult AddRoleToUser(int userId, string roleName)
        {
            var user = _repository.Get<User>(userId);
            var role = _repository.FindByPredicate<Role>(x => x.RoleName == roleName);
            if (user == null)
                return new OperationResult(false, "User not found");
            if (role == null)
                return new OperationResult(false, "Role not found");
            
            var result = _repository.Create(new UserRole(user, role));
            
            return result == null
                ? new OperationResult(false, "Create error")
                : new OperationResult(true, result);
        }
    }
}