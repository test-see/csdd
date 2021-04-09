using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.client.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nouns.client.profile;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class ClientApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Client_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Client/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ListClientRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListClientResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Client_AddAndDelete_ReturnIntAsync()
        {
            var Client = await _rootpath
                .AppendPathSegment("/api/Client/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CreateClientRequest { Name = "q" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Client>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Client/{Client.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task Client_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Client/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UpdateClientRequest { Name = "q" })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task Client_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Client/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<GetClientResponse>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

    }
}
