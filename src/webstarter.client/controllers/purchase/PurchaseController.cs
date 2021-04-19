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
    public class PurchaseController : DefaultControllerBase
    {
        private readonly IPurchaseService _PurchaseService;
        public PurchaseController(IPurchaseService PurchaseService)
        {
            _PurchaseService = PurchaseService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<PurchaseListQueryModel> query)
        {
            var data = await _PurchaseService.GetPagerListAsync(query);
            return Json(data);
        }

    }
}
