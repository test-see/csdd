using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using irespository.purchase.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.purchase
{
    [TestClass]
    public class PurchaseGoodsTest : BaseApiTest
    {
        [TestMethod]
        public async Task PurchaseGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PurchaseGoodsListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PurchaseGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task PurchaseGoods_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseGoodsCreateApiModel
                {
                    Qty = 1,
                    HospitalGoodsId = 1,
                    PurchaseId = 1,
                    HospitalClientId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.PurchaseGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/PurchaseGoods/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task PurchaseGoods_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseGoodsUpdateApiModel { Qty = 1,  HospitalClientId = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task PurchaseGoods_HospitalGoods_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/goods")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalGoodsListQueryModel> {  })
                .ReceiveJson<OkMessage<PagerResult<HospitalGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }

        [TestMethod]
        public async Task PurchaseGoods_ThresholdType_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/client")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalClientListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<HospitalClientListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
    }
}
