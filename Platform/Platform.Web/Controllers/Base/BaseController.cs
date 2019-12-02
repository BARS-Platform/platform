using System;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
<<<<<<< HEAD
=======
using Platform.Fodels.Interfaces;
>>>>>>> Реализована пагинация на сервере
using Platform.Services.Common;

namespace Platform.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IRepository Repository;

        public BaseController(IRepository repository) => Repository = repository;

        protected IActionResult HandleRequest(Func<OperationResult> function)
        {
            var result = function();
            return result.Success ? (IActionResult) Ok(result) : Conflict(result);
        }
<<<<<<< HEAD
=======
        
        /// <summary>
        /// Метод для получения объекта.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Get(int id) => Ok(Repository.Get<T>(id));
        
        /// <summary>
        /// Метод для получения объектов.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetAll(ListParam listParam) => Ok(Repository.GetAll<T>().FormData(listParam));
        
        /// <summary>
        /// Метод для создания объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public virtual IActionResult Create(T entity) => Ok(Repository.Create(entity));
        
        /// <summary>
        /// Метод для изменения объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Update(T entity) => Ok(Repository.Update(entity));
        
        /// <summary>
        /// Метод для удаления объекта.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public virtual IActionResult Delete(T entity) => Ok(Repository.Delete(entity));
>>>>>>> Реализована пагинация на сервере
    }
}