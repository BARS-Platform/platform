using Microsoft.AspNetCore.Authorization;

namespace Platform.Services.Requirements
{
    public class RoleRequirement: IAuthorizationRequirement
    {
        public string Name { get; }

        public RoleRequirement(string roleName)
        {
            Name = roleName;
        }   
    }
}