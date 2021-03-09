using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireClientRole")]
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
            var data = _hospitalGoodsService.GetPagerList(query);
            return Json(data);
        }
    }
}
