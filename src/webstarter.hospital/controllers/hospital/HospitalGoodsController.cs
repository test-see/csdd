using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
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


        [HttpGet]
        [Route("index")]
        public JsonResult GetIndexByBarcode(string barcode)
        {
            var data = _hospitalGoodsService.GetValueByBarcode(barcode);
            return Json(data);
        }
    }
}
