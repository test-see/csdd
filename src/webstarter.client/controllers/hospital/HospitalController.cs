using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireClientRole")]
    public class HospitalController : DefaultControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalListQueryModel> query)
        {
            var data = _hospitalService.GetPagerList(query);
            return Json(data);
        }

    }
}
