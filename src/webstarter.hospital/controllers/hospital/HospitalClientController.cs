using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.client.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireHospitalRole")]
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
            query.Query.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _HospitalClientService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _HospitalClientService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(HospitalClientCreateApiModel created)
        {
            created.HospitalId = HospitalDepartment.Hospital.Id;
            var data = _HospitalClientService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, HospitalClientUpdateApiModel updated)
        {
            var data = _HospitalClientService.Update(id, updated, Profile.Id);
            return Json(data);
        }

    }
}
