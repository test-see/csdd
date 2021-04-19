using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.purchase
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PurchaseGoodsBillnoController : DefaultControllerBase
    {
        private readonly IPurchaseGoodsBillnoService _PurchaseGoodsBillnoService;
        public PurchaseGoodsBillnoController(IPurchaseGoodsBillnoService PurchaseGoodsBillnoService)
        {
            _PurchaseGoodsBillnoService = PurchaseGoodsBillnoService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            var data = await _PurchaseGoodsBillnoService.GetPagerListByHospitalAsync(query, HospitalDepartment.Hospital.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("comfirm")]
        public async Task<JsonResult> ComfirmAsync(IList<int> ids)
        {
            var data = await _PurchaseGoodsBillnoService.ComfirmAsync(ids, Profile.Id);
            return Json(data);
        }
    }
}
