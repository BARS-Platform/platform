using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Platform.Fatabase;
using Platform.Fodels.Models;

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
                .FindAllByPredicate<RolePermission>(rolePermission => roleIds.Any(x => x == rolePermission.Role.Id),
                    query => query
                        .Include(x => x.Role)
                        .Include(x => x.Permission))
                .Select(x => x.Permission)
                .ToList();
        }
    }
}