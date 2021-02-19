using csdd.Controllers.Shared;
using foundation.config;
using irespository.purchase.model;
using iservice.purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public JsonResult GetList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            var data = _PurchaseGoodsBillnoService.GetPagerListByHospitalDepartment(query, HospitalDepartment.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("comfirm")]
        public JsonResult Comfirm(IList<int> ids)
        {
            var data = _PurchaseGoodsBillnoService.Comfirm(ids, Profile.Id);
            return Json(data);
        }
    }
}
