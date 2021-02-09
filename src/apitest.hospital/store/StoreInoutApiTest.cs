using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.storeinout.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.StoreInout
{
    [TestClass]
    public class StoreInoutTest : BaseApiTest
    {
        [TestMethod]
        public async Task StoreInout_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<StoreInoutListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<StoreInoutListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task StoreInout_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/StoreInout/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutCreateApiModel { Name = "1", Remark = "2" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.StoreInout>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/StoreInout/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task StoreInout_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
