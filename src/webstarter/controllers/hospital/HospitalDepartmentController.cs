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
        private readonly IHospitalDepartmentService _hospitalDepartmentService;
        public HospitalDepartmentController(IHospitalDepartmentService HospitalDepartmentService)
        {
            _hospitalDepartmentService = HospitalDepartmentService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<HospitalDepartmentListQueryModel> query)
        {
            var data = _hospitalDepartmentService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _hospitalDepartmentService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(HospitalDepartmentCreateApiModel created)
        {
            var data = _hospitalDepartmentService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, HospitalDepartmentUpdateApiModel updated)
        {
            var data = _hospitalDepartmentService.Update(id, updated, UserId);
            return Json(data);
        }

        [HttpGet]
        [Route("type")]
        public JsonResult GetDepartmentTypeList()
        {
            var data = _hospitalDepartmentService.GetDepartmentTypeList();
            return Json(data);
        }

        [HttpGet]
        [Route("parent")]
        public JsonResult GetParentList()
        {
            var data = _hospitalDepartmentService.GetParentList();
            return Json(data);
        }
    }
}
