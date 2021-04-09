using csdd.Controllers.Shared;
using irespository.client.goods.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/ClientMappingGoods")]
    public class ClientGoods2HospitalGoodsController : DefaultControllerBase
    {
        private readonly IClientGoods2HospitalGoodsService _clientMappingGoodsService;
        public ClientGoods2HospitalGoodsController(IClientGoods2HospitalGoodsService clientMappingGoodsService)
        {
            _clientMappingGoodsService = clientMappingGoodsService;
        }

        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _clientMappingGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(ClientGoods2HospitalGoodsCreateApiModel created)
        {
            var data = _clientMappingGoodsService.Create(created, UserId);
            return Json(data);
        }

    }
}
