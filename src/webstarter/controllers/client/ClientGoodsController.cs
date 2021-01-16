using csdd.Controllers.Shared;
using foundation.config;
using irespository.client.goods.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientGoodsController : DefaultControllerBase
    {
        private readonly IClientGoodsService _clientGoodsService;
        public ClientGoodsController(IClientGoodsService ClientGoodsService)
        {
            _clientGoodsService = ClientGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<ClientGoodsListQueryModel> query)
        {
            var data = _clientGoodsService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _clientGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(ClientGoodsCreateApiModel created)
        {
            var data = _clientGoodsService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, ClientGoodsUpdateApiModel updated)
        {
            var data = _clientGoodsService.Update(id, updated, UserId);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _clientGoodsService.GetIndex(id);
            return Json(data);
        }
    }
}
