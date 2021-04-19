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
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireClientRole")]
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
            query.Query = query.Query ?? new ListClientGoodsRequest { };
            query.Query.ClientId = Client.Id;
            var data = await _mediator.RequestPagerListAsync<ListClientGoodsRequest, ListClientGoodsResponse>(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteClientGoodsCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientGoodsRequest created)
        {
            created.UserId = Profile.Id;
            created.ClientId = Client.Id;
            var data = await _mediator.RequestPipeAsync<CreateClientGoodsRequest, ClientGoods>(created);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public async Task<JsonResult> UpdateAsync(int id, UpdateClientGoodsRequest updated)
        {
            updated.Id = id;
            updated.UserId = Profile.Id;
            var data = await _mediator.RequestPipeAsync<UpdateClientGoodsRequest, ClientGoods>(updated);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetAsync(int id)
        {
            var data = await _mediator.RequestSingleByIdAsync<GetClientGoodsRequest, GetClientGoodsResponse>(id);
            return Json(data);
        }
    }
}
