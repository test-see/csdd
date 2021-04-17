using csdd.Controllers.Shared;
using foundation.config;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireClientRole")]
    public class HospitalGoodsController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalGoodsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListHospitalGoodsRequest> query)
        {
            query.Query = query.Query ?? new ListHospitalGoodsRequest { };
            var data = await _mediator.RequestPagerListAsync<ListHospitalGoodsRequest, ListHospitalGoodsResponse>(query);
            return Json(data);
        }
    }
}
