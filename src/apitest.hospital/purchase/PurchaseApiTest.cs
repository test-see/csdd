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
    public class PurchaseTest : BaseApiTest
    {
        [TestMethod]
        public async Task Purchase_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<PurchaseListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<PurchaseListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Purchase_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/Purchase/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseCreateApiModel { Name = "1", Remark = "2" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Purchase>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Purchase/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task Purchase_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PurchaseUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task Purchase_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<PurchaseIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task Purchase_Submit_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/1/submit")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task Purchase_Comfirm_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/1/comfirm")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task Purchase_Back_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Purchase/1/back")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
