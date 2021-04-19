using csdd.Controllers.Shared;
using foundation.config;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireClientRole")]
    public class HospitalController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListHospitalRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListHospitalRequest, ListHospitalResponse>(query);
            return Json(data);
        }
    }
}
