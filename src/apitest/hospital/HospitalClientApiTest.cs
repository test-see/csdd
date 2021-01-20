using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.client.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.hospital
{
    [TestClass]
    public class HospitalClientApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task HospitalClient_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalClient/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalClientListQueryModel>
                {
                    Query = new HospitalClientListQueryModel
                    {
                        HospitalId = 1,
                    }
                })
                .ReceiveJson<OkMessage<PagerResult<HospitalClientListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task HospitalClient_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalClient/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalClientCreateApiModel
                {
                    Name = "q",
                    HospitalId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.HospitalClient>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/HospitalClient/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task HospitalClient_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalClient/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalClientUpdateApiModel
                {
                    Name = "qxx",
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


    }
}