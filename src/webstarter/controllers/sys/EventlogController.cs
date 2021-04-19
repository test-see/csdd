using csdd.Controllers.Shared;
using foundation.config;
using foundation.mediator;
using irespository.sys.model;
using iservice.sys;
using Mediator.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    public class EventlogController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public EventlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListEventlogRequest> query)
        {
            var data = await _mediator.ListByPageAsync<ListEventlogRequest, ListEventlogResponse>(query);
            return Json(data);
        }
    }
}
