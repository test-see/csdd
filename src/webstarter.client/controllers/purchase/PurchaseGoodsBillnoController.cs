﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.client.model;
using irespository.purchase.model;
using iservice.hospital;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireClientRole")]
    public class PurchaseGoodsBillnoController : DefaultControllerBase
    {
        private readonly IPurchaseGoodsBillnoService _PurchaseGoodsBillnoService;
        public PurchaseGoodsBillnoController(IPurchaseGoodsBillnoService PurchaseGoodsBillnoService)
        {
            _PurchaseGoodsBillnoService = PurchaseGoodsBillnoService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            var data = _PurchaseGoodsBillnoService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _PurchaseGoodsBillnoService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(PurchaseGoodsBillnoCreateApiModel created)
        {
            var data = _PurchaseGoodsBillnoService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            var data = _PurchaseGoodsBillnoService.Update(id, updated);
            return Json(data);
        }

    }
}