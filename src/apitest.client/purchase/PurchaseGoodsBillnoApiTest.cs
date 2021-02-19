using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.client.model;
using irespository.purchase.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace apitest.purchase
{
    [TestClass]
    public class PurchaseGoodsBillnoTest : BaseApiTest
    {
        [TestMethod]
        public async Task PurchaseGoodsBillno_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoodsBillno/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PurchaseGoodsBillnoListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PurchaseGoodsBillnoListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task PurchaseGoodsBillno_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/PurchaseGoodsBillno/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseGoodsBillnoCreateApiModel
                {
                    Qty = 1,
                    PurchaseGoodsId = 1,
                    Billno = "1",
                    Enddate = DateTime.Now,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.PurchaseGoodsBillno>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/PurchaseGoodsBillno/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task PurchaseGoodsBillno_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoodsBillno/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseGoodsBillnoUpdateApiModel { Qty = 1, Billno = "1", Enddate = DateTime.Now, })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

    }
}
