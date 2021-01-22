using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireDefaultRole")]
    [EnableCors("csdd")]
    public class DefaultControllerBase : ControllerBase
    {
        protected int UserId => int.Parse(HttpContext.User.Identity.Name);
        //protected int DepartmentId => int.Parse(HttpContext.User.Identity.);
        protected JsonResult Json<T>(T d)
        {
            return new JsonResult(new OkMessage<T>(d));
        }
    }
}
