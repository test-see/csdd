﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseSettingController : DefaultControllerBase
    {
        private readonly IPurchaseSettingService _purchaseSettingService;
        public PurchaseSettingController(IPurchaseSettingService purchaseSettingService)
        {
            _purchaseSettingService = purchaseSettingService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PurchaseSettingListQueryModel> query)
        {
            var data = _purchaseSettingService.GetPagerList(query, HospitalDepartment.Hospital.Id);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _purchaseSettingService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(PurchaseSettingCreateApiModel created)
        {
            var data = _purchaseSettingService.Create(created, HospitalDepartment.Id, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseSettingUpdateApiModel updated)
        {
            var data = _purchaseSettingService.Update(id, updated);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _purchaseSettingService.GetIndex(id);
            return Json(data);
        }
    }
}
