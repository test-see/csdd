﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.storeinout.model;
using iservice.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.StoreInout
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class StoreInoutController : DefaultControllerBase
    {
        private readonly IStoreInoutService _storeInoutService;
        public StoreInoutController(IStoreInoutService storeInoutService)
        {
            _storeInoutService = storeInoutService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<StoreInoutListQueryModel> query)
        {
            var data = await _storeInoutService.GetPagerListAsync(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _storeInoutService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(StoreInoutCreateApiModel created)
        {
            var data = _storeInoutService.Create(created, HospitalDepartment.Id, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, StoreInoutUpdateApiModel updated)
        {
            var data = _storeInoutService.Update(id, updated);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetIndexAsync(int id)
        {
            var data = await _storeInoutService.GetIndexAsync(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public async Task<JsonResult> SubmitAsync(int id)
        {
            var data = await _storeInoutService.SubmitAsync(id, Profile.Id);
            return Json(data);
        }

        [HttpGet]
        [Route("changetype")]
        public JsonResult GetCustomizeChangeTypeList()
        {
            var data = _storeInoutService.GetCustomizeChangeTypeList();
            return Json(data);
        }
    }
}
