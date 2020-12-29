using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using iservice.sys;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    public class ConfigController : DefaultControllerBase
    {
        private readonly IConfigService _configService;
        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<ConfigListQueryModel> query)
        {
            var data = _configService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _configService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(ConfigCreateApiModel created)
        {
            var data = _configService.Create(created, UserId);
            return Json(data);
        }
    }
}
