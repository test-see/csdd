using csdd.Controllers.Shared;
using foundation.config;
using irespository.checklist.model;
using iservice.checklist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
        public async Task<JsonResult> GetListAsync(PagerQuery<CheckListQueryModel> query)
        {
            var data = await _CheckListService.GetPagerListAsync(query, HospitalDepartment.Hospital.Id);
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
        public async Task<JsonResult> PostAsync(CheckListCreateApiModel created)
        {
            var data = await _CheckListService.CreateAsync(created, Profile.Id);
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
        public async Task<JsonResult> GetIndexAsync(int id)
        {
            var data = await _CheckListService.GetIndexAsync(id);
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
        public async Task<JsonResult> GetPreviewListAsync(int id, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            var data = await _CheckListService.GetPagerPreviewListAsync(id, query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/bill")]
        public async Task<JsonResult> BillAsync(int id)
        {
            var data = await _CheckListService.BillAsync(id, Profile.Id);
            return Json(data);
        }
    }
}
