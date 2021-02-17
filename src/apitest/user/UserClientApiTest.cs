using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.user.client.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.user
{
    [TestClass]
    public class UserClientApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task UserClient_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/UserClient/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<UserClientListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<UserClientListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task UserClient_AddAndDelete_ReturnIntAsync()
        {
            var UserClient = await _rootpath
                .AppendPathSegment("/api/UserClient/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserClientCreateApiModel { Name = "q", ClientId = 1, UserId = 1 })
                .ReceiveJson<OkMessage<foundation.ef5.poco.UserClient>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/UserClient/{UserClient.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
