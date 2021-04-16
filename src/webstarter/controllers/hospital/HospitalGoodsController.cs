using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using iservice.hospital;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using storage.hospitalgoods.carrier;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
            var data = await _mediator.RequestPagerListAsync<ListHospitalGoodsRequest, ListHospitalGoodsResponse>(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteHospitalGoods { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalGoods created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateHospitalGoods, HospitalGoods>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalGoods updated)
        {
            updated.Id = id;
            var data = await _mediator.RequestPipeAsync<UpdateHospitalGoods, HospitalGoods>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.RequestSingleByIdAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(id);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/inactive")]
        public async Task<JsonResult> UpdateInActiveAsync(int id)
        {
            var updated = new UpdateHospitalGoodsIsActive { Id = id, IsActive = false, };
            var data = await _mediator.RequestPipeAsync<UpdateHospitalGoodsIsActive, HospitalGoods>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/active")]
        public async Task<JsonResult> UpdateActiveAsync(int id)
        {
            var updated = new UpdateHospitalGoodsIsActive { Id = id, IsActive = true, };
            var data = await _mediator.RequestPipeAsync<UpdateHospitalGoodsIsActive, HospitalGoods>(updated);
            return Json(data);
        }
    }
}
