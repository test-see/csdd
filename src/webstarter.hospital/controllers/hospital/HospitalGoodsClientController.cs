using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.hospital
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class HospitalGoodsClientController : DefaultControllerBase
    {
        private readonly IHospitalGoodsClientService _hospitalGoodsClientService;
        public HospitalGoodsClientController(IHospitalGoodsClientService hospitalGoodsClientService)
        {
            _hospitalGoodsClientService = hospitalGoodsClientService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalGoodsClientQueryModel> query)
        {
            var data = _hospitalGoodsClientService.GetPagerList(query);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _hospitalGoodsClientService.Delete(id);
            return Json(data);
        }


        [HttpGet]
        [Route("add")]
        public JsonResult Post(int goodsId, int clientId)
        {
            var data = _hospitalGoodsClientService.Create(goodsId, clientId, Profile.Id);
            return Json(data);
        }

    }
}
