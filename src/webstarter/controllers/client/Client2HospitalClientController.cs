using csdd.Controllers.Shared;
using domain.client.profile.entity;
using domain.v2.client;
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
        private readonly ClientService _clientService;
        public Client2HospitalClientController(IMediator mediator,
            ClientService clientService)
        {
            _mediator = mediator;
            _clientService = clientService;
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<OkMessage<int>> DeleteAsync(int id)
        {
            await _clientService.HospitalClientService.DeleteAsync(id);
            return OkMessage(id);
        }

        // 待续
        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClient2HospitalClientRequest> query)
        {
            var data = await _mediator.ListByPageAsync<ListClient2HospitalClientRequest, ListClient2HospitalClientResponse>(query);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClient2HospitalClientRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.ToPipeAsync<CreateClient2HospitalClientRequest, Client2HospitalClient>(created);
            return Json(data);
        }
    }
}
