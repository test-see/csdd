using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class HospitalController : DefaultControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService HospitalService)
        {
            _hospitalService = HospitalService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalListQueryModel> query)
        {
            var data = _hospitalService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _hospitalService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(HospitalCreateApiModel created)
        {
            var data = _hospitalService.Create(created, UserId);
            return Json(data);
        }
    }
}
