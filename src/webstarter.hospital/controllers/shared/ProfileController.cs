using irespository.user.enums;
using iservice.sys;
using iservice.user;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Shared
{
    public class ProfileController : DefaultControllerBase
    {
        private readonly IUserHospitalService _userHospitalService;
        private readonly IRoleService _roleService;
        public ProfileController(IUserHospitalService userHospitalService,
            IRoleService roleService)
        {
            _userHospitalService = userHospitalService;
            _roleService = roleService;
        }
        [HttpGet]
        [Route("index")]
        public async Task<JsonResult> GetProfileAsync()
        {
            var data = await _userHospitalService.GetIndexByUserIdAsync(Profile.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("menu/list")]
        public JsonResult GetMenuListByUserId()
        {
            var data = _roleService.GetMenuListByUserId((int)Portal.Hospital, Profile.Id);
            return Json(data);
        }
    }
}
