using domain.user.valuemodel;
using foundation.config;
using irespository.user.profile.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace csdd.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireDefaultRole")]
    [EnableCors("csdd")]
    public class DefaultControllerBase : ControllerBase
    {
        protected UserValueModel Profile => JsonConvert.DeserializeObject<UserValueModel>(HttpContext.User.Identity.Name);
        protected LoginHospitalValueModel HospitalDepartment => JsonConvert.DeserializeObject<LoginHospitalValueModel>(HttpContext.User.Claims.First(x => x.Type == "HospitalDepartmentId").Value);
        protected JsonResult Json<T>(T d)
        {
            return new JsonResult(new OkMessage<T>(d));
        }
    }
}
