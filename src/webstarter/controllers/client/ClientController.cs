using csdd.Controllers.Shared;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
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
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<ListingClientRequest> query)
        {
            var data = await _mediator.RequestAsync<PagerQuery<ListingClientRequest>, PagerResult<ListingClientResponse>>(query);
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
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientRequest updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.RequestAsync<UpdateClientRequest, Client>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.RequestAsync<GetClientRequest, GetClientResponse>(new GetClientRequest(id));
            return Json(data);
        }

    }
}
