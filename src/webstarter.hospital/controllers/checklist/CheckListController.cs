using csdd.Controllers.Shared;
using foundation.config;
using irespository.checklist.model;
using iservice.checklist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace webstarter.hospital.controllers.CheckList
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class CheckListController : DefaultControllerBase
    {
        private readonly ICheckListService _CheckListService;
        private readonly ILogger<CheckListController> _logger;
        public CheckListController(ICheckListService CheckListService,ILogger<CheckListController> logger)
        {
            _CheckListService = CheckListService;
            _logger = logger;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<CheckListQueryModel> query)
        {
            var data = _CheckListService.GetPagerListAsync(query, HospitalDepartment.Hospital.Id);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _CheckListService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(CheckListCreateApiModel created)
        {
            var data = _CheckListService.CreateAsync(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, CheckListUpdateApiModel updated)
        {
            var data = _CheckListService.Update(id, updated);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _CheckListService.GetIndexAsync(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public JsonResult Submit(int id)
        {
            var data = _CheckListService.Submit(id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/preview")]
        public JsonResult GetPreviewList(int id, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            var data = _CheckListService.GetPagerPreviewListAsync(id, query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/bill")]
        public JsonResult Bill(int id)
        {
            var data = _CheckListService.BillAsync(id, Profile.Id);
            return Json(data);
        }
    }
}
