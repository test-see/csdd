using csdd.Controllers.Shared;
using foundation.config;
using irespository.prescription.model;
using iservice.prescription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<JsonResult> GetListAsync(PagerQuery<PrescriptionListQueryModel> query)
        {
            var data = await _prescriptionService.GetPagerListAsync(query, HospitalDepartment.Hospital.Id);
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
        public async Task<JsonResult> SubmitAsync(int id)
        {
            var data = await _prescriptionService.SubmitAsync(id, Profile.Id);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetIndexAsync(int id)
        {
            var data = await _prescriptionService.GetIndexAsync(id);
            return Json(data);
        }
    }
}
