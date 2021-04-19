using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using storage.hospitalgoods.carrier;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireHospitalRole")]
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
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = await _mediator.ListByPageAsync<ListHospitalGoodsRequest, ListHospitalGoodsResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("store/list")]
        public async Task<JsonResult> ListStoreAsync(PagerQuery<ListHospitalGoodsStoreRequest> query)
        {
            query.Query = query.Query ?? new ListHospitalGoodsStoreRequest { };
            query.Query.HospitalDepartmentId = HospitalDepartment.Id;
            var data = await _mediator.ListByPageAsync<ListHospitalGoodsStoreRequest, ListHospitalGoodsStoreResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("index/query")]
        public async Task<JsonResult> GetByBarcodeAsync(string barcode)
        {
            var data = await _mediator.GetAsync<GetHospitalGoodsByBarcodeRequest, GetHospitalGoodsResponse>(
                new GetHospitalGoodsByBarcodeRequest(barcode));
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.GetByIdAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.ToPipeAsync(new DeleteHospitalGoodsCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalGoodsRequest created)
        {
            created.UserId = Profile.Id;
            created.HospitalId = HospitalDepartment.Hospital.Id;
            var data = await _mediator.ToPipeAsync<CreateHospitalGoodsRequest, HospitalGoods>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalGoodsRequest updated)
        {
            updated.Id = id;
            var data = await _mediator.ToPipeAsync<UpdateHospitalGoodsRequest, HospitalGoods>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/inactive")]
        public async Task<JsonResult> UpdateInActiveAsync(int id)
        {
            var updated = new UpdateHospitalGoodsIsActiveRequest { Id = id, IsActive = false, };
            var data = await _mediator.ToPipeAsync<UpdateHospitalGoodsIsActiveRequest, HospitalGoods>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/active")]
        public async Task<JsonResult> UpdateActiveAsync(int id)
        {
            var updated = new UpdateHospitalGoodsIsActiveRequest { Id = id, IsActive = true, };
            var data = await _mediator.ToPipeAsync<UpdateHospitalGoodsIsActiveRequest, HospitalGoods>(updated);
            return Json(data);
        }
    }
}
