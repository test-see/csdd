﻿using csdd.Controllers.Shared;
using foundation.config;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using iservice.invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult GetList(PagerQuery<InvoiceListQueryModel> query)
        {
            var data = _InvoiceService.GetPagerList(query);
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
            var data = _InvoiceService.Create(created, HospitalDepartment.Id, Profile.Id);
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
        public JsonResult GetIndex(int id)
        {
            var data = _InvoiceService.GetIndex(id);
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
        public JsonResult Generate(int id)
        {
            var data = _InvoiceService.Generate(id);
            return Json(data);
        }

        [HttpPost]
        [Route("list/report")]
        public JsonResult GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            var data = _InvoiceService.GetPagerReportList(query);
            return Json(data);
        }

        [HttpPost]
        [Route("list/record")]
        public JsonResult GetPagerReportRecordList(PagerQuery<InvoiceReportRecordQueryApiModel> query)
        {
            var data = _InvoiceService.GetPagerStoreRecordList(query);
            return Json(data);
        }

    }
}
