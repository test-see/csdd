using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class ClientMappingApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task ClientMapping_AddAndDelete_ReturnIntAsync()
        {
            var Client = await _rootpath
                .AppendPathSegment("/api/ClientMapping/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new Client2HospitalClientCreateApiModel { ClientId = 1, HospitalClientId = 1 })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Client2HospitalClient>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/ClientMapping/{Client.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task ClientGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/ClientMapping/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<Client2HospitalClientListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<ListClient2HospitalClientResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
    }
}
