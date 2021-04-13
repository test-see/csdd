﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.client.goods.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireClientRole")]
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
            query.Query = query.Query ?? new ClientGoodsListQueryModel { };
            query.Query.ClientId = Client.Id;
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
        public JsonResult Post(CreateClientGoods created)
        {
            created.ClientId = Client.Id;
            var data = _clientGoodsService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, UpdateClientGoods updated)
        {
            var data = _clientGoodsService.Update(id, updated, Profile.Id);
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
