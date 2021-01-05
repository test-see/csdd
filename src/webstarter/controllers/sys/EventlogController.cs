using csdd.Controllers.Shared;
using foundation.config;
using irespository.sys.model;
using iservice.sys;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    public class EventlogController : DefaultControllerBase
    {
        private readonly IEventlogService _eventlogService;
        public EventlogController(IEventlogService eventlogService)
        {
            _eventlogService = eventlogService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<EventlogListQueryModel> query)
        {
            var data = _eventlogService.GetPagerList(query);
            return Json(data);
        }
    }
}
