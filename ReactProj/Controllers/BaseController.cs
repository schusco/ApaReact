using Microsoft.AspNetCore.Mvc;

namespace ReactProj.Controllers
{
    public class BaseController(IRepository repository) : ControllerBase
    {
        protected IRepository Repository { get; set; } = repository;
    }
}
