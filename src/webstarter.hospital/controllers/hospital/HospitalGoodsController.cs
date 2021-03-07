using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class HospitalGoodsController : DefaultControllerBase
    {
        private readonly IHospitalGoodsService _hospitalGoodsService;
        public HospitalGoodsController(IHospitalGoodsService hospitalGoodsService)
        {
            _hospitalGoodsService = hospitalGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            query.Query = query.Query ?? new HospitalGoodsListQueryModel { };
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _hospitalGoodsService.GetPagerList(query);
            return Json(data);
        }
        [HttpPost]
        [Route("store/list")]
        public JsonResult GetPagerStoreList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            query.Query = query.Query ?? new HospitalGoodsListQueryModel { };
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _hospitalGoodsService.GetPagerStoreList(query, HospitalDepartment.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("index/query")]
        public JsonResult GetIndexByBarcode(string barcode)
        {
            var data = _hospitalGoodsService.GetValueByBarcode(barcode);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _hospitalGoodsService.GetIndex(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _hospitalGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(HospitalGoodsCreateApiModel created)
        {
            created.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _hospitalGoodsService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, HospitalGoodsUpdateApiModel updated)
        {
            var data = _hospitalGoodsService.Update(id, updated, Profile.Id);
            return Json(data);
        }



        [HttpGet]
        [Route("{id}/inactive")]
        public JsonResult UpdateInActive(int id)
        {
            var data = _hospitalGoodsService.UpdateIsActive(id, false);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/active")]
        public JsonResult UpdateActive(int id)
        {
            var data = _hospitalGoodsService.UpdateIsActive(id, true);
            return Json(data);
        }
    }
}
