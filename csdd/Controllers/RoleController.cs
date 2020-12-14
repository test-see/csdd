using csdd.Controllers.Shared;
using foundation.config;
using iservice.sys;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers
{
    public class RoleController : DefaultControllerBase
    {
        private readonly IRoleService  _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("list")]
        public OkMessage GetList()
        {
            var data = _roleService.GetList();
            return new OkMessage(data);
        }
    }
}
