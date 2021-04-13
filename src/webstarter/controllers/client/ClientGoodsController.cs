using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using irespository.client.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientGoodsController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public ClientGoodsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListClientGoodsRequest> query)
        {
            var data = await _mediator.RequestAsync<StorageRequest<PagerQuery<ListClientGoodsRequest>>, PagerResult<ListClientGoodsResponse>>(
                new StorageRequest<PagerQuery<ListClientGoodsRequest>>(query));
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendAsync(new PipeCommand<DeleteClientGoods>(new DeleteClientGoods { Id = id }));
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientGoods created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestAsync<PipeRequest<CreateClientGoods>, ClientGoods>(new PipeRequest<CreateClientGoods>(created));
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientGoods updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.RequestAsync<PipeRequest<UpdateClientGoods>, ClientGoods>(new PipeRequest<UpdateClientGoods>(updated));
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var request = new StorageRequest<GetClientGoodsRequest>(new GetClientGoodsRequest(id));
            var data = await _mediator.RequestAsync<StorageRequest<GetClientGoodsRequest>, GetResponse<GetClientGoodsResponse>>(request);
            return Json(data.Payloads.FirstOrDefault());
        }
    }
}
