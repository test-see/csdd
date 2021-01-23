using iservice.user;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Shared
{
    public class ProfileController : DefaultControllerBase
    {
        private readonly IUserHospitalService _userHospitalService;
        public ProfileController(IUserHospitalService userHospitalService)
        {
            _userHospitalService = userHospitalService;
        }
        [HttpGet]
        [Route("index")]
        public JsonResult GetProfile()
        {
            var data = _userHospitalService.GetIndexByUserId(Profile.Id);
            return Json(data);
        }
    }
}
