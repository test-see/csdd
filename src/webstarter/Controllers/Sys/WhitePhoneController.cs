using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using iservice.sys;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    public class WhitePhoneController : DefaultControllerBase
    {
        private readonly IWhitePhoneService _whitePhoneService;
        public WhitePhoneController(IWhitePhoneService whitePhoneService)
        {
            _whitePhoneService = whitePhoneService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<WhitePhoneListQueryModel> query)
        {
            var data = _whitePhoneService.GetPagerList(query);
            return Json(data);
        }
    }
}
