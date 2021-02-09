using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.client.model;
using irespository.storeinout.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.storeinout
{
    [TestClass]
    public class StoreInoutGoodsTest : BaseApiTest
    {
        [TestMethod]
        public async Task StoreInoutGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInoutGoods/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<StoreInoutGoodsListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<StoreInoutGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task StoreInoutGoods_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/StoreInoutGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutGoodsCreateApiModel
                {
                    Qty = 1,
                    HospitalGoodsId = 1,
                    StoreInoutId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.StoreInoutGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/StoreInoutGoods/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task StoreInoutGoods_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInoutGoods/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutGoodsUpdateApiModel { Qty = 1, })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

    }
}
