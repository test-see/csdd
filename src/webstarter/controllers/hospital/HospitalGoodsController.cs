using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.goods.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
            var data = _hospitalGoodsService.GetPagerList(query);
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
            var data = _hospitalGoodsService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, HospitalGoodsUpdateApiModel updated)
        {
            var data = _hospitalGoodsService.Update(id, updated, UserId);
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
