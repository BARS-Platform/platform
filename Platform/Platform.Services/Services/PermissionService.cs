using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Dto;

namespace Platform.Services.Services
{
    public class PermissionService
    {
        private readonly IRepository _repository;
        
        public PermissionService(IRepository repository)
        {
            _repository = repository;
        }
        
        public List<Permission> GetPermissions(ClaimsPrincipal principal)
        {
            var roleNames = principal.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);

            var roleIds = _repository
                .FindAllByPredicate<Role>(role => roleNames.Contains(role.RoleName))
                .Select(x => x.Id);
            
            return _repository
                .FindAllByPredicate<RolePermission>(rolePermission => roleIds.Any(x => x == rolePermission.Role.Id))
                .Select(x => x.Permission)
                .ToList();
        }

        public OperationResult GetAll(ListParam listParam)
        {
            var result = _repository
                .GetAll<Permission>()
                .Select(PermissionDto.ProjectionExpression)
                .FormData(listParam);

            return result == null
                ? new OperationResult(false, "No permissions found")
                : new OperationResult(true, result);
        }
        
        public OperationResult AddPermissionToRole(int roleId, int permissionId) {
            var role = _repository.Get<Role>(roleId);
            var permission = _repository.Get<Permission>(permissionId);
            if (role == null)
                return new OperationResult(false, "Role not found");
            if (permission == null)
                return new OperationResult(false, "Permission not found");

            var result = _repository.Create(new RolePermission(role, permission));
            return result == null
                ? new OperationResult(false, "Create error")
                : new OperationResult(true, result);
        }
        
        public OperationResult AddPermissionToRole(int roleId, string permissionName) {
            var role = _repository.Get<Role>(roleId);
            var permission = _repository.FindByPredicate<Permission>(x => x.PermissionId == permissionName);
            if (role == null)
                return new OperationResult(false, "Role not found");
            if (permission == null)
                return new OperationResult(false, "Permission not found");

            var result = _repository.Create(new RolePermission(role, permission));
            return result == null
                ? new OperationResult(false, "Create error")
                : new OperationResult(true, result);
        }
    }
}