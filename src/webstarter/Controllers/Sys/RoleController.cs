using csdd.Controllers.Shared;
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
        public JsonResult GetList()
        {
            var data = _roleService.GetList();
            return Json(data);
        }

        [HttpPost]
        public JsonResult Post(string name)
        {
            var data = _roleService.Create(name, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("delete")]
        public JsonResult Delete(int id)
        {
            var data = _roleService.Delete(id);
            return Json(data);
        }
    }
}
