using csdd.Controllers.Shared;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using iservice.client;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nouns.client.profile;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMediator _mediator;
        public ClientController(IClientService clientService,
            IMediator mediator)
        {
            _clientService = clientService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<ListClientRequest> query)
        {
            var data = await _mediator.RequestAsync<PagerQuery<ListClientRequest>, PagerResult<ListClientResponse>>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new DeleteClientCommand(id));
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestAsync<CreateClientRequest, Client>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, ClientUpdateApiModel updated)
        {
            var data = _clientService.Update(id, updated, UserId);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _clientService.GetIndex(id);
            return Json(data);
        }

    }
}
