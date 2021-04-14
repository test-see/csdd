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
            var data = await _mediator.RequestAsync<StorageRequest<PagerQuery<ListHospitalClientRequest>>, PagerResult<ListHospitalClientResponse>>(
                new StorageRequest<PagerQuery<ListHospitalClientRequest>>(query));
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateHospitalClient updated)
        {
            updated.Id = id;
            var data = await _mediator.RequestAsync<PipeRequest<UpdateHospitalClient>, HospitalClient>(new PipeRequest<UpdateHospitalClient>(updated));
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new PipeCommand<DeleteHospitalClient>(new DeleteHospitalClient { Id = id }));
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateHospitalClient created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestAsync<PipeRequest<CreateHospitalClient>, HospitalClient>(new PipeRequest<CreateHospitalClient>(created));
            return Json(data);
        }
    }
}
