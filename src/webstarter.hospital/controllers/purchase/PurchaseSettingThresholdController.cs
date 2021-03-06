using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.purchase.model;
using iservice.hospital;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseSettingThresholdController : DefaultControllerBase
    {
        private readonly IPurchaseSettingThresholdService _purchaseSettingThresholdService;
        private readonly IHospitalGoodsService _hospitalGoodsService;
        public PurchaseSettingThresholdController(IPurchaseSettingThresholdService purchaseSettingThresholdService,
            IHospitalGoodsService hospitalGoodsService)
        {
            _purchaseSettingThresholdService = purchaseSettingThresholdService;
            _hospitalGoodsService = hospitalGoodsService;
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

        [HttpPost]
        [Route("goods")]
        public JsonResult GetHospitalGoodsList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            query.Query = query.Query ?? new HospitalGoodsListQueryModel { };
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _hospitalGoodsService.GetPagerList(query);
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
