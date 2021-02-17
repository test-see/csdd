using iservice.user;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Shared
{
    public class ProfileController : DefaultControllerBase
    {
        private readonly IUserClientService _userClientService;
        public ProfileController(IUserClientService userClientService)
        {
            _userClientService = userClientService;
        }
        [HttpGet]
        [Route("index")]
        public JsonResult GetProfile()
        {
            var data = _userClientService.GetIndexByUserId(Profile.Id);
            return Json(data);
        }
    }
}
