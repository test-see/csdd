using csdd.Controllers.Shared;
using foundation.config;
using irespository.checklist.model;
using iservice.checklist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.CheckList
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class CheckListGoodsController : DefaultControllerBase
    {
        private readonly ICheckListGoodsService _CheckListGoodsService;
        public CheckListGoodsController(ICheckListGoodsService CheckListGoodsService)
        {
            _CheckListGoodsService = CheckListGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<CheckListGoodsQueryModel> query)
        {
            var data = _CheckListGoodsService.GetPagerListAsync(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _CheckListGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(CheckListGoodsCreateApiModel created)
        {
            var data = _CheckListGoodsService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, CheckListGoodsUpdateApiModel updated)
        {
            var data = _CheckListGoodsService.Update(id, updated);
            return Json(data);
        }

    }
}
