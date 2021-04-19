using csdd.Controllers.Shared;
using foundation.config;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using iservice.invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.invoice
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class InvoiceController : DefaultControllerBase
    {
        private readonly IInvoiceService _InvoiceService;
        public InvoiceController(IInvoiceService InvoiceService)
        {
            _InvoiceService = InvoiceService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<InvoiceListQueryModel> query)
        {
            var data = await _InvoiceService.GetPagerListAsync(query, HospitalDepartment.Hospital.Id);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _InvoiceService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(InvoiceCreateApiModel created)
        {
            created.HospitalDepartmentId = HospitalDepartment.Id;
            var data = _InvoiceService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, InvoiceUpdateApiModel updated)
        {
            var data = _InvoiceService.Update(id, updated);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public async Task<JsonResult> GetIndexAsync(int id)
        {
            var data = await _InvoiceService.GetIndexAsync(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/submit")]
        public JsonResult Submit(int id)
        {
            var data = _InvoiceService.Submit(id);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}/generate")]
        public async Task<JsonResult> GenerateAsync(int id)
        {
            var data = await _InvoiceService.GenerateAsync(id);
            return Json(data);
        }

        [HttpPost]
        [Route("list/report")]
        public JsonResult GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            var data = _InvoiceService.GetPagerReportList(query);
            return Json(data);
        }

        [HttpGet]
        [Route("type")]
        public JsonResult GetInvoiceTypeList()
        {
            var data = _InvoiceService.GetInvoiceTypeList();
            return Json(data);
        }

        [HttpPost]
        [Route("index/report/list")]
        public async Task<JsonResult> GetPagerRecordListByReportIdAsync(PagerQuery<int> query)
        {
            var data = await _InvoiceService.GetPagerRecordListByReportIdAsync(query);
            return Json(data);
        }

        [HttpPost]
        [Route("index/storerecord/list")]
        public async Task<JsonResult> GetPagerRecordListByInvoiceIdAsync(PagerQuery<int> query)
        {
            var data = await _InvoiceService.GetPagerRecordListByInvoiceIdAsync(query);
            return Json(data);
        }
    }
}
