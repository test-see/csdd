using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apitest.Info
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
                .PostJsonAsync(new PagerQuery<HospitalListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<HospitalListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task Hospital_AddAndDelete_ReturnIntAsync()
        {
            var role = await _rootpath
                .AppendPathSegment("/api/Config/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalCreateApiModel { Name = "q", Remark = "d" })
                .ReceiveJson<OkMessage<Hospital>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Hospital/{role.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
