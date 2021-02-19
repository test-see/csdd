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
    }
}
