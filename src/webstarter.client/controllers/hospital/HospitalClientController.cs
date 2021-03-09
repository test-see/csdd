using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.client.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireClientRole")]
    public class HospitalClientController : DefaultControllerBase
    {
        private readonly IHospitalClientService _HospitalClientService;
        public HospitalClientController(IHospitalClientService HospitalClientService)
        {
            _HospitalClientService = HospitalClientService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalClientListQueryModel> query)
        {
            query.Query = query.Query ?? new HospitalClientListQueryModel { };
            var data = _HospitalClientService.GetPagerList(query);
            return Json(data);
        }


    }
}
