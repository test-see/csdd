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
            var data = await _mediator.RequestPagerListAsync<ListClientGoodsRequest, ListClientGoodsResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteClientGoods { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientGoods created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateClientGoods, ClientGoods>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientGoods updated)
        {
            updated.Id = id;
            updated.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<UpdateClientGoods, ClientGoods>(updated);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.RequestSingleAsync<GetClientGoodsRequest, GetClientGoodsResponse>(new GetClientGoodsRequest(id));
            return Json(data);
        }
    }
}
