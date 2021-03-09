using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.client.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
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


    }
}