using iservice.sys;
using iservice.user;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Shared
{
    public class ProfileController : DefaultControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public ProfileController(IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpGet]
        [Route("index")]
        public JsonResult GetProfile()
        {
            var data = _userService.GetIndex(UserId);
            return Json(data);
        }
        [HttpGet]
        [Route("menu/list")]
        public JsonResult GetMenuListByUserId()
        {
            var data = _roleService.GetMenuListByUserId(UserId);
            return Json(data);
        }
    }
}
