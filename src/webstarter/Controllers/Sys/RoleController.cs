using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using iservice.sys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _roleService.Delete(id);
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
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _roleService.GetIndex(id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, RoleIndexUpdateModel updated)
        {
            var data = _roleService.Update(id, updated);
            return Json(data);
        }

    }
}
