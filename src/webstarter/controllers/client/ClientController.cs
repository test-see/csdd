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
            var request = new StorageRequest<GetClientRequest>(new GetClientRequest(id));
            var data = await _mediator.RequestAsync<StorageRequest<GetClientRequest>, GetResponse<GetClientResponse>>(request);
            return Json(data.Payloads.FirstOrDefault());
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClient updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.RequestAsync<PipeRequest<UpdateClient>, Client>(new PipeRequest<UpdateClient>(updated));
            return Json(data);
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClientRequest> query)
        {
            var data = await _mediator.RequestAsync<StorageRequest<PagerQuery<ListClientRequest>>, PagerResult<ListClientResponse>>(
                new StorageRequest<PagerQuery<ListClientRequest>>(query));
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new PipeCommand<DeleteClient>(new DeleteClient { Id = id }));
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClient created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestAsync<PipeRequest<CreateClient>, Client>(new PipeRequest<CreateClient>(created));
            return Json(data);
        }
    }
}
