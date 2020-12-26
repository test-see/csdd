using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
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

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<RoleListQueryModel> query)
        {
            var data = _roleService.GetPagerList(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{roleId}/delete")]
        public JsonResult Delete(int roleId)
        {
            var data = _roleService.Delete(roleId);
            return Json(data);
        }
    
    
        [HttpPost]
        [Route("add")]
        public JsonResult Post(RoleCreateApiModel created)
        {
            var data = _roleService.Create(created, UserId);
            return Json(data);
        }

        [HttpGet]
        [Route("menu")]
        public JsonResult GetMenuList()
        {
            var data = _roleService.GetMenuList();
            return Json(data);
        }

        [HttpGet]
        [Route("{roleId}/index")]
        public JsonResult GetRoleIndex(int roleId)
        {
            var data = _roleService.GetRoleIndex(roleId);
            return Json(data);
        }

        [HttpPost]
        [Route("update")]
        public JsonResult UpdateRole(RoleIndexUpdateModel updated)
        {
            var data = _roleService.UpdateRole(updated);
            return Json(data);
        }

    }
}
