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
    public class PurchaseSettingThresholdTest : BaseApiTest
    {
        [TestMethod]
        public async Task PurchaseSettingThreshold_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseSettingThreshold/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PurchaseSettingThresholdListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PurchaseSettingThresholdListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task PurchaseSettingThreshold_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/PurchaseSettingThreshold/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseSettingThresholdCreateApiModel { DownQty = 1, UpQty = 1, HospitalDepartmentId = 1 })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Hospital>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/PurchaseSettingThreshold/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task PurchaseSettingThreshold_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseSettingThreshold/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseSettingThresholdUpdateApiModel { DownQty = 1, UpQty = 1, })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
