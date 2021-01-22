using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace csdd.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireDefaultRole")]
    [EnableCors("csdd")]
    public class DefaultControllerBase : ControllerBase
    {
        protected int UserId => int.Parse(HttpContext.User.Identity.Name);
        protected int HospitalDepartmentId => int.Parse(HttpContext.User.Claims.First(x => x.Type == "HospitalDepartmentId").Value);
        protected int HospitalId => int.Parse(HttpContext.User.Claims.First(x => x.Type == "HospitalId").Value);
        protected JsonResult Json<T>(T d)
        {
            return new JsonResult(new OkMessage<T>(d));
        }
    }
}
