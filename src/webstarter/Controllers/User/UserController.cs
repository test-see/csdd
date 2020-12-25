using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.User
{
    public class UserController : DefaultControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<UserListQueryModel> query)
        {
            var data = _userService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("role")]
        public JsonResult GetUserRoleList(int roleId)
        {
            var data = _userService.GetUserRoleList(roleId);
            return Json(data);
        }

        [HttpPost]
        [Route("role/update")]
        public JsonResult UpdateUserRoleList(UserRoleListUpdateModel Updated)
        {
            var data = _userService.UpdateUserRoleList(Updated);
            return Json(data);
        }
    }
}
