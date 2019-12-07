using System;
using Microsoft.AspNetCore.Mvc;
using Platform.Fatabase;
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
    }
}