using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.purchase.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.purchase
{
    [TestClass]
    public class PurchaseSettingTest : BaseApiTest
    {
        [TestMethod]
        public async Task PurchaseSetting_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseSetting/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PurchaseSettingListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PurchaseSettingListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task PurchaseSetting_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/PurchaseSetting/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseSettingCreateApiModel { Name = "1", Remark = "2" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.PurchaseSetting>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/PurchaseSetting/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task PurchaseSetting_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseSetting/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseSettingUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
