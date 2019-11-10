using Microsoft.AspNetCore.Authorization;

namespace Platform.Services.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Name { get; }

        public PermissionRequirement(string permissionName)
        {
            Name = permissionName;
        }
    }
}