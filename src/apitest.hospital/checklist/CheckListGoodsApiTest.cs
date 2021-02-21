using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.checklist.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.checklist
{
    [TestClass]
    public class CheckListGoodsTest : BaseApiTest
    {
        [TestMethod]
        public async Task CheckListGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckListGoods/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<CheckListGoodsQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<CheckListGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task CheckListGoods_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/CheckListGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CheckListGoodsCreateApiModel
                {
                    CheckQty = 1,
                    HospitalGoodsId = 1,
                    CheckListId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.CheckListGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/CheckListGoods/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task CheckListGoods_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckListGoods/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CheckListGoodsUpdateApiModel { CheckQty = 1, })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
