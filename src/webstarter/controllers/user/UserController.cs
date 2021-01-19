using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using irespository.user.model;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.User
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _userService.GetIndex(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/inactive")]
        public JsonResult UpdateInActive(int id)
        {
            var data = _userService.UpdateIsActive(id, false);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/active")]
        public JsonResult UpdateActive(int id)
        {
            var data = _userService.UpdateIsActive(id, true);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> AddAsync(UserCreateApiModel created)
        {
            var user = await _userService.AddAsync(created, base.UserId);
            return Json(user.Id);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, UserUpdateApiModel updated)
        {
            var data = _userService.Update(id, updated);
            return Json(data);
        }

    }
}
