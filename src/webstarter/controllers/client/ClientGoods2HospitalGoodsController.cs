using csdd.Controllers.Shared;
using domain.client.goods2hospitalgoods.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/ClientMappingGoods")]
    public class ClientGoods2HospitalGoodsController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public ClientGoods2HospitalGoodsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteClientGoods2HospitalGoodsCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateClientGoods2HospitalGoodsRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateClientGoods2HospitalGoodsRequest, ClientGoods2HospitalGoods>(created);
            return Json(data);
        }

    }
}
