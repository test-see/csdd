using csdd.Controllers.Shared;
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

        [HttpGet]
        [Route("list")]
        public JsonResult GeListByGoodsId(int goodsId)
        {
            var data = _hospitalGoodsClientService.GeListByGoodsId(goodsId);
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
