using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/ClientMapping")]
    public class Client2HospitalClientController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public Client2HospitalClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClient2HospitalClientRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListClient2HospitalClientRequest, ListClient2HospitalClientResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteClient2HospitalClient { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClient2HospitalClient created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateClient2HospitalClient, Client2HospitalClient>(created);
            return Json(data);
        }
    }
}
