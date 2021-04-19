using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireClientRole")]
    public class PurchaseGoodsController : DefaultControllerBase
    {
        private readonly IPurchaseGoodsService _purchaseGoodsService;
        public PurchaseGoodsController(IPurchaseGoodsService purchaseGoodsService)
        {
            _purchaseGoodsService = purchaseGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<PurchaseGoodsListQueryModel> query)
        {
            var data = await _purchaseGoodsService.GetPagerListByClientAsync(query, Client.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetIndexAsync(int id)
        {
            var data = await _purchaseGoodsService.GetIndexAsync(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public async Task<JsonResult> SubmitAsync(int id)
        {
            var data = await _purchaseGoodsService.SubmitAsync(id);
            return Json(data);
        }
    }
}
