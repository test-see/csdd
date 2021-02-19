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
            query.Query = query.Query ?? new PurchaseGoodsListQueryModel { };
            query.Query.ClientId = Client.Id;
            var data = _PurchaseGoodsService.GetPagerList(query);
            return Json(data);
        }

    }
}
