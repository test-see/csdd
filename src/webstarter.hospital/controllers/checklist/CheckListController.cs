using csdd.Controllers.Shared;
using foundation.config;
using irespository.checklist.model;
using iservice.checklist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.CheckList
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class CheckListController : DefaultControllerBase
    {
        private readonly ICheckListService _CheckListService;
        public CheckListController(ICheckListService CheckListService)
        {
            _CheckListService = CheckListService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<CheckListQueryModel> query)
        {
            var data = _CheckListService.GetPagerList(query);
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
            var data = _CheckListService.Create(created, HospitalDepartment.Id, Profile.Id);
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
            var data = _CheckListService.GetIndex(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public JsonResult Submit(int id)
        {
            var data = _CheckListService.Submit(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/bill")]
        public JsonResult Bill(int id)
        {
            var data = _CheckListService.Bill(id);
            return Json(data);
        }
    }
}
