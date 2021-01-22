using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult GetList(PagerQuery<PurchaseSettingThresholdListQueryModel> query)
        {
            var data = _purchaseSettingThresholdService.GetPagerList(query);
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
            var data = _purchaseSettingThresholdService.Create(created, HospitalDepartmentId, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseSettingThresholdUpdateApiModel updated)
        {
            var data = _purchaseSettingThresholdService.Update(id, updated);
            return Json(data);
        }
    }
}
