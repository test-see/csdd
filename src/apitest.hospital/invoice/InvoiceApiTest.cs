using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.invoice.model;
using irespository.invoice.profile.enums;
using irespository.store.profile.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Invoice
{
    [TestClass]
    public class InvoiceTest : BaseApiTest
    {
        [TestMethod]
        public async Task Invoice_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<InvoiceListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<InvoiceListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Invoice_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/Invoice/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new InvoiceCreateApiModel
                {
                    Name = "1",
                    Remark = "2",
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    InvoiceTypeId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Invoice>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Invoice/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task Invoice_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new InvoiceUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task Invoice_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<InvoiceIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task Invoice_Submit_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/1/submit")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task Invoice_Generate_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/1/generate")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task Invoice_Report_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/list/report")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<InvoiceReportQueryApiModel> { Query = new InvoiceReportQueryApiModel { InvoiceId = 1 } })
                .ReceiveJson<OkMessage<PagerResult<InvoiceReportListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Invoice_Record_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/list/record")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<InvoiceReportRecordQueryApiModel> { Query = new InvoiceReportRecordQueryApiModel { InvoiceReportId = 1 } })
                .ReceiveJson<OkMessage<PagerResult<StoreRecordListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }


        [TestMethod]
        public async Task User_InvoiceType_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Invoice/type")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataInvoiceType>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }
    }
}
