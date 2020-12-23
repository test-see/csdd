using csdd.Controllers.Shared;
using iservice.sys;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    public class PrivilegeController : DefaultControllerBase
    {
        private readonly IPrivilegeService _privilegeService;
        public PrivilegeController(IPrivilegeService privilegeService)
        {
            _privilegeService = privilegeService;
        }

        [HttpGet]
        [Route("list")]
        public JsonResult GetList()
        {
            var data = _privilegeService.GetList();
            return Json(data);
        }
    }
}
