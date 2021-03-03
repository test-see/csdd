using irespository.user.enums;
using iservice.sys;
using iservice.user;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Shared
{
    public class ProfileController : DefaultControllerBase
    {
        private readonly IUserClientService _userClientService;
        private readonly IRoleService _roleService;
        public ProfileController(IUserClientService userClientService,
            IRoleService roleService)
        {
            _userClientService = userClientService;
            _roleService = roleService;
        }
        [HttpGet]
        [Route("index")]
        public JsonResult GetProfile()
        {
            var data = _userClientService.GetIndexByUserId(Profile.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("menu/list")]
        public JsonResult GetMenuListByUserId()
        {
            var data = _roleService.GetMenuListByUserId((int)AuthorizeRole.Client, Profile.Id);
            return Json(data);
        }
    }
}
