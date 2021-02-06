using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using irespository.purchase.model;
using iservice.hospital;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseGoodsController : DefaultControllerBase
    {
        private readonly IPurchaseGoodsService _PurchaseGoodsService;
        private readonly IHospitalGoodsService _hospitalGoodsService;
        private readonly IHospitalClientService _hospitalClientService; 
        public PurchaseGoodsController(IPurchaseGoodsService PurchaseGoodsService,
            IHospitalGoodsService hospitalGoodsService,
            IHospitalClientService hospitalClientService)
        {
            _PurchaseGoodsService = PurchaseGoodsService;
            _hospitalGoodsService = hospitalGoodsService;
            _hospitalClientService = hospitalClientService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            var data = _PurchaseGoodsService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _PurchaseGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(PurchaseGoodsCreateApiModel created)
        {
            var data = _PurchaseGoodsService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseGoodsUpdateApiModel updated)
        {
            var data = _PurchaseGoodsService.Update(id, updated);
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


        [HttpPost]
        [Route("client")]
        public JsonResult GetHospitalClientList(PagerQuery<HospitalClientListQueryModel> query)
        {
            query.Query = query.Query ?? new HospitalClientListQueryModel { };
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _hospitalClientService.GetPagerList(query);
            return Json(data);
        }
    }
}
