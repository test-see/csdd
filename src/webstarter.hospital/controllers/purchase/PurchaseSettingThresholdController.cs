﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseSettingThresholdController : DefaultControllerBase
    {
        private readonly IPurchaseSettingThresholdService _purchaseSettingThresholdService;
        public PurchaseSettingThresholdController(IPurchaseSettingThresholdService purchaseSettingThresholdService)
        {
            _purchaseSettingThresholdService = purchaseSettingThresholdService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            var data = await _purchaseSettingThresholdService.GetPagerListAsync(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _purchaseSettingThresholdService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(PurchaseSettingThresholdCreateApiModel created)
        {
            var data = _purchaseSettingThresholdService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            var data = _purchaseSettingThresholdService.Update(id, updated);
            return Json(data);
        }

        [HttpGet]
        [Route("thresholdtype")]
        public JsonResult GetThresholdTypeList()
        {
            var data = _purchaseSettingThresholdService.GetThresholdTypeList();
            return Json(data);
        }
    }
}
