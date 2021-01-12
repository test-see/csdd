using csdd.Controllers.Shared;
using foundation.config;
using irespository.hospital.department.model;
using iservice.hospital;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class HospitalDepartmentController : DefaultControllerBase
    {
        private readonly IHospitalDepartmentService _HospitalDepartmentService;
        public HospitalDepartmentController(IHospitalDepartmentService HospitalDepartmentService)
        {
            _HospitalDepartmentService = HospitalDepartmentService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            var data = _HospitalDepartmentService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _HospitalDepartmentService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(HospitalDepartmentCreateApiModel created)
        {
            var data = _HospitalDepartmentService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, HospitalDepartmentUpdateApiModel updated)
        {
            var data = _HospitalDepartmentService.Update(id, updated, UserId);
            return Json(data);
        }

    }
}
