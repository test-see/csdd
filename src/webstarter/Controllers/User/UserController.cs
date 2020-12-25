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


        [HttpGet]
        [Route("{userId}/active")]
        public JsonResult UpdateActive(int userId)
        {
            var data = _userService.UpdateIsActive(userId, true);
            return Json(data);
        }


        [HttpGet]
        [Route("{userId}/inactive")]
        public JsonResult UpdateInActive(int userId)
        {
            var data = _userService.UpdateIsActive(userId, false);
            return Json(data);
        }
    }
}
