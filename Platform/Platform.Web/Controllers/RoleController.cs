using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Models;
using Platform.Web.Controllers.Base;

namespace Platform.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseController<Role>
    {
        public RoleController(IRepository repository)
            :base (repository)
        {
        }

        [Authorize("RoleEdit")]
        public override Role Create(Role entity) => base.Create(entity);

        [Authorize("RoleEdit")]
        public override Role Update(Role entity) => base.Update(entity);

        [Authorize("RoleEdit")]
        public override bool Delete(Role entity) => base.Delete(entity);
    }
}
