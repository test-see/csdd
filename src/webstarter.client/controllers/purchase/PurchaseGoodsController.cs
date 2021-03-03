using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireClientRole")]
    public class PurchaseGoodsController : DefaultControllerBase
    {
        private readonly IPurchaseGoodsService _PurchaseGoodsService;
        public PurchaseGoodsController(IPurchaseGoodsService PurchaseGoodsService)
        {
            _PurchaseGoodsService = PurchaseGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            var data = _PurchaseGoodsService.GetPagerMappingList(query, Client.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _PurchaseGoodsService.GetIndex(id);
            return Json(data);
        }
    }
}
