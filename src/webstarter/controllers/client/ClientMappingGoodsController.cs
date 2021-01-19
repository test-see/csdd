using csdd.Controllers.Shared;
using irespository.client.goods.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientMappingGoodsController : DefaultControllerBase
    {
        private readonly IClientMappingGoodsService _clientMappingGoodsService;
        public ClientMappingGoodsController(IClientMappingGoodsService clientMappingGoodsService)
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
        public JsonResult Post(ClientMappingGoodsCreateApiModel created)
        {
            var data = _clientMappingGoodsService.Create(created, UserId);
            return Json(data);
        }

    }
}
