using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.client.model;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireHospitalRole")]
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
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = await _mediator.RequestPagerListAsync<ListHospitalClientRequest, ListHospitalClientResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteHospitalClientCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalClientRequest created)
        {
            created.UserId = Profile.Id;
            created.HospitalId = HospitalDepartment.Hospital.Id;
            var data = await _mediator.RequestPipeAsync<CreateHospitalClientRequest, HospitalClient>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalClientRequest updated)
        {
            updated.Id = id;
            var data = await _mediator.RequestPipeAsync<UpdateHospitalClientRequest, HospitalClient>(updated);
            return Json(data);
        }
    }
}
