using csdd.Controllers.Shared;
using foundation.config;
using foundation.mediator;
using irespository.hospital.client.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireClientRole")]
    public class HospitalClientController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalClientController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListHospitalClientRequest> query)
        {
            query.Query = query.Query ?? new ListHospitalClientRequest { };
            var data = await _mediator.RequestPagerListAsync<ListHospitalClientRequest, ListHospitalClientResponse>(query);
            return Json(data);
        }

    }
}
