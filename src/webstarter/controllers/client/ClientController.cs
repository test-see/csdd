using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nouns.client.profile;
using System.Linq;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.RequestSingleByIdAsync<GetClientRequest, GetClientResponse>(id);
            return Json(data);
        }
        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClientRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListClientRequest, ListClientResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateClientRequest, Client>(created);
            return Json(data);
        }
        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientRequest updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<UpdateClientRequest, Client>(updated);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteClientCommand { Id = id });
            return Json(id);
        }
    }
}
