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


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _whitePhoneService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(WhitePhoneCreateApiModel created)
        {
            var data = _whitePhoneService.Create(created, UserId);
            return Json(data);
        }
    }
}
