using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.prescription.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apitest.prescription
{
    [TestClass]
    public class PrescriptionApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Prescription_List_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Prescription/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PrescriptionListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PrescriptionListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Prescription_Add_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Prescription/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PrescriptionCreateApiModel
                {
                    Cardno = "11",
                    HospitalGoods = new List<KeyValuePair<int, int>> { new KeyValuePair<int, int>(1, 1) },
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Prescription>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task Prescription_Submit_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Prescription/1/submit")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task Prescription_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Prescription/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<PrescriptionIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }
    }
}
