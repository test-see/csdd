using csdd.Controllers.Shared;
using foundation.config;
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
    }
}
