using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.department.model;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using storage.data.carrier;
using storage.hospital.department.carrier;
using storage.hospitaldepartment.carrier;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class HospitalDepartmentController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalDepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListHospitalDepartmentRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListHospitalDepartmentRequest, ListHospitalDepartmentResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalDepartmentRequest updated)
        {
            updated.Id = id;
            var data = await _mediator.RequestPipeAsync<UpdateHospitalDepartmentRequest, HospitalDepartment>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteHospitalDepartmentCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalDepartmentRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateHospitalDepartmentRequest, HospitalDepartment>(created);
            return Json(data);
        }

        [HttpGet]
        [Route("parent")]
        public async Task<JsonResult> ListParentAsync()
        {
            var data = await _mediator.RequestListAsync<ListParentHospitalDepartmentRequest, IdNameValueModel>();
            return Json(data);
        }

        [HttpGet]
        [Route("type")]
        public async Task<JsonResult> ListTypeAsync()
        {
            var data = await _mediator.RequestListAsync<ListDepartmentTypeRequest, IdNameValueModel>();
            return Json(data);
        }
    }
}
