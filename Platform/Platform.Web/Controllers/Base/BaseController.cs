using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
using Platform.Fodels.Interfaces;

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
        public virtual T Get(int id) => Repository.Get<T>(id);
        
        /// <summary>
        /// Метод для получения объектов.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IEnumerable<T> GetAll() => Repository.GetAll<T>();
        
        /// <summary>
        /// Метод для создания объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public virtual T Create(T entity) => Repository.Create(entity);
        
        /// <summary>
        /// Метод для изменения объекта.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual T Update(T entity) => Repository.Update(entity);
        
        /// <summary>
        /// Метод для удаления объекта.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public virtual bool Delete(T entity) => Repository.Delete(entity);
    }
}