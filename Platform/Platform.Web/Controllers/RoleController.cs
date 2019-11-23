using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Platform.Fatabase;
using Platform.Fodels.Entities;
using Platform.Fodels.Models;
using Platform.Services.Services;

namespace Platform.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RoleController : Controller
    {
        private readonly IRepository _repository;
        
        public RoleController(IRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Метод для получения ролей.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Role> GetAll()
        {
            var roles = _repository.FindAllByPredicate<Role>(x => true);
            return roles;
        }
    }
}
