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
using storage.adapter.v2.client;
using System.Linq;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClientRespository _clientRespository;
        public ClientController(IMediator mediator,
            IClientRespository clientRespository)
        {
            _mediator = mediator;
            _clientRespository = clientRespository;
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<OkMessage<GetClientResponse>> GetAsync(int id)
        {
            var data = await _clientRespository.GetOverviewByIdAsync(id);
            return OkMessage(new GetClientResponse
            {
                CreateTime = data.Client.CreateTime,
                CreateUserName = data.User.Username,
                Id = data.Client.Id,
                Name = data.Client.Name,
            });
        }
        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClientRequest> query)
        {
            var data = await _mediator.ListByPageAsync<ListClientRequest, ListClientResponse>(query);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.ToPipeAsync<CreateClientRequest, Client>(created);
            return Json(data);
        }
        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientRequest updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.ToPipeAsync<UpdateClientRequest, Client>(updated);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.ToPipeAsync(new DeleteClientCommand { Id = id });
            return Json(id);
        }
    }
}
