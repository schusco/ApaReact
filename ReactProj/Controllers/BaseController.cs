using Microsoft.AspNetCore.Mvc;

namespace ReactProj.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(IRepository repository)
        {
            Repository = repository;
        }
        protected IRepository Repository { get; set; }
    }
}
