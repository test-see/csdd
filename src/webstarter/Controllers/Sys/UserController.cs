using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [Route("{userId}/index")]
        public JsonResult GetUserIndex(int userId)
        {
            var data = _userService.GetUserIndex(userId);
            return Json(data);
        }

        [HttpGet]
        [Route("{userId}/inactive")]
        public JsonResult UpdateInActive(int userId)
        {
            var data = _userService.UpdateIsActive(userId, false);
            return Json(data);
        }

        [HttpGet]
        [Route("{userId}/active")]
        public JsonResult UpdateActive(int userId)
        {
            var data = _userService.UpdateIsActive(userId, true);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> AddActiveUserAsync(UserCreateApiModel created)
        {
            var user = await _userService.AddActiveUserAsync(created, base.UserId);
            return Json(user.Id);
        }

        [HttpPost]
        [Route("update")]
        public JsonResult UpdateUser(UserUpdateApiModel Updated)
        {
            var data = _userService.UpdateUser(Updated);
            return Json(data);
        }

        [HttpGet]
        [Route("profile")]
        public JsonResult GetProfile()
        {
            var data = _userService.GetUserIndex(UserId);
            return Json(data);
        }

        [HttpGet]
        [Route("data/authorize")]
        public JsonResult GetDataAuthorizeList()
        {
            var data = _userService.GetDataAuthorizeList();
            return Json(data);
        }
    }
}
