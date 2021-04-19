using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
            var data = await _mediator.ListByPageAsync<ListHospitalRequest, ListHospitalResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalRequest updated)
        {
            updated.Id = id;
            var data = await _mediator.ToPipeAsync<UpdateHospitalRequest, Hospital>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.ToPipeAsync(new DeleteHospitalCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.ToPipeAsync<CreateHospitalRequest, Hospital>(created);
            return Json(data);
        }
    }
}
