using csdd.Controllers.Shared;
using foundation.config;
using irespository.prescription.model;
using iservice.prescription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.prescription
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class PrescriptionController : DefaultControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<PrescriptionListQueryModel> query)
        {
            var data = _prescriptionService.GetPagerListAsync(query, HospitalDepartment.Hospital.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public JsonResult Post(PrescriptionCreateApiModel created)
        {
            var data = _prescriptionService.Create(created, HospitalDepartment.Id, Profile.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/submit")]
        public JsonResult Submit(int id)
        {
            var data = _prescriptionService.SubmitAsync(id, Profile.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _prescriptionService.GetIndexAsync(id);
            return Json(data);
        }
    }
}
