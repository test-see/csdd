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
    [Authorize(Policy = "RequireAdministratorRole")]
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
            var data = await _mediator.ListByPageAsync<ListHospitalClientRequest, ListHospitalClientResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalClientRequest updated)
        {
            updated.Id = id;
            var data = await _mediator.ToPipeAsync<UpdateHospitalClientRequest, HospitalClient>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.ToPipeAsync(new DeleteHospitalClientCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalClientRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.ToPipeAsync<CreateHospitalClientRequest, HospitalClient>(created);
            return Json(data);
        }
    }
}
