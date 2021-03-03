using apitest.shared;
using domain.purchase.valuemodel;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.purchase.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                .PostJsonAsync(new PagerQuery<PurchaseGoodsListQueryModel> { Query= new PurchaseGoodsListQueryModel { HospitalClientId=1 } })
                .ReceiveJson<OkMessage<PagerResult<PurchaseGoodsMappingListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task PurchaseGoods_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/PurchaseGoods/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<PurchaseGoodsListQueryModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }
    }
}
