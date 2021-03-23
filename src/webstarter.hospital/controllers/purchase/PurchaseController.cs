using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseController : DefaultControllerBase
    {
        private readonly IPurchaseService _PurchaseService;
        public PurchaseController(IPurchaseService PurchaseService)
        {
            _PurchaseService = PurchaseService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PurchaseListQueryModel> query)
        {
            query = query ?? new PagerQuery<PurchaseListQueryModel> { };
            query.Query = query.Query ?? new PurchaseListQueryModel { };
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _PurchaseService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _PurchaseService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(PurchaseCreateApiModel created)
        {
            var data = _PurchaseService.Create(created, HospitalDepartment.Id, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, PurchaseUpdateApiModel updated)
        {
            var data = _PurchaseService.Update(id, updated);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _PurchaseService.GetIndex(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public JsonResult Submit(int id)
        {
            var data = _PurchaseService.Submit(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/comfirm")]
        public JsonResult Comfirm(int id)
        {
            var data = _PurchaseService.Comfirm(id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/back")]
        public JsonResult Revoke(int id)
        {
            var data = _PurchaseService.Revoke(id);
            return Json(data);
        }
    }
}
