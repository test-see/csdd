﻿using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.hospital
{
    [TestClass]
    public class HospitalApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Hospital_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Hospital/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ListHospitalRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListHospitalResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Hospital_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/Hospital/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CreateHospitalRequest { Name = "q", Remark = "d" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Hospital>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Hospital/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task Hospital_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Hospital/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UpdateHospitalRequest { Name = "q", Remark = "d" })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
