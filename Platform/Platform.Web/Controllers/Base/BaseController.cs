using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Models.Address;
using Platform.Services.Common;
using Platform.Services.Dto;

namespace Platform.Web.Controllers.Base
{
    public class BaseController<T> : Controller
        where T : class, IEntityBase
    {
        protected readonly IRepository Repository;

        public BaseController(IRepository repository)
        {
            Repository = repository;
        }
        
        /// <summary>
        /// Метод для получения объекта.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult Get(int id) => Ok(Repository.Get<T>(id));
        
        /// <summary>
        /// Метод для получения объектов.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult GetAll(ListParam listParam) => Ok(Repository.GetAll<T>().FormData(listParam));
        
        /// <summary>
        /// Метод для создания объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult Create(T entity) => Ok(Repository.Create(entity));
        
        /// <summary>
        /// Метод для изменения объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult Update(T entity) => Ok(Repository.Update(entity));
        
        /// <summary>
        /// Метод для удаления объекта.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult Delete(T entity) => Ok(Repository.Delete(entity));
    }
}