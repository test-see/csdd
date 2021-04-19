﻿using foundation.config;
using foundation.ef5.poco;
using irespository.data;
using irespository.invoice;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.invoice.report.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.invoice
{
    public class InvoiceContext
    {
        private readonly IInvoiceRespository _InvoiceRespository;
        private readonly IInvoiceReportRespository _invoiceReportRespository;
        private readonly IInvoiceTypeRespository _invoiceTypeRespository;
        public InvoiceContext(IInvoiceRespository InvoiceRespository,
            IInvoiceReportRespository invoiceReportRespository,
            IInvoiceTypeRespository invoiceTypeRespository)
        {
            _InvoiceRespository = InvoiceRespository;
            _invoiceReportRespository = invoiceReportRespository;
            _invoiceTypeRespository = invoiceTypeRespository;
        }

        public PagerResult<InvoiceListApiModel> GetPagerList(PagerQuery<InvoiceListQueryModel> query, int hospitalId)
        {
            return _InvoiceRespository.GetPagerList(query, hospitalId);
        }
        public Invoice Create(InvoiceCreateApiModel created, int userId)
        {
            return _InvoiceRespository.Create(created, userId);
        }

        public int Generate(int invoiceId)
        {
            var invoice = _InvoiceRespository.GetIndex(invoiceId);
            var reports = new List<InvoiceReportValueModel>();
            if (invoice.InvoiceType.Id == (int)InvoiceType.Client)
            {
                reports = _invoiceReportRespository.GetInvoiceListByClient(invoice);
            }
            if (invoice.InvoiceType.Id == (int)InvoiceType.ChangeType)
            {
                reports = _invoiceReportRespository.GetInvoiceListByChangeType(invoice);
            }

            _invoiceReportRespository.Generate(invoiceId, reports);
            return _InvoiceRespository.UpdateStatus(invoiceId, InvoiceStatus.UnSubmited);
        }

        public int Delete(int id)
        {
            return _InvoiceRespository.Delete(id);
        }
        public int Update(int id, InvoiceUpdateApiModel updated)
        {
            return _InvoiceRespository.Update(id, updated);
        }
        public InvoiceIndexApiModel GetIndex(int id)
        {
            var goods = _InvoiceRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _InvoiceRespository.UpdateStatus(id, InvoiceStatus.Submited);
        }

        public PagerResult<InvoiceReportListApiModel> GetPagerReportList(PagerQuery<InvoiceReportQueryApiModel> query)
        {
            return _invoiceReportRespository.GetPagerList(query);
        }
        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByReportIdAsync(PagerQuery<int> query)
        {
            return await _invoiceReportRespository.GetPagerRecordListByReportIdAsync(query);
        }
        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerRecordListByInvoiceIdAsync(PagerQuery<int> query)
        {
            return await _invoiceReportRespository.GetPagerRecordListByInvoiceIdAsync(query);
        }

        public IEnumerable<DataInvoiceType> GetInvoiceTypeList()
        {
            return _invoiceTypeRespository.GetList();
        }
    }
}
