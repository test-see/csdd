using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using irespository.checklist.profile.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.checklist
{
    [TestClass]
    public class CheckListTest : BaseApiTest
    {
        [TestMethod]
        public async Task CheckList_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<CheckListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<CheckListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task CheckList_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/CheckList/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CheckListCreateApiModel { Name = "1", Remark = "2" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.CheckList>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/CheckList/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task CheckList_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CheckListUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task CheckList_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<CheckListIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task CheckList_Submit_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/1/submit")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task CheckList_Bill_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/1/bill")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task CheckList_Preview_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/CheckList/1/preview")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<CheckListGoodsPreviewQueryModel> { })
                .ReceiveJson<OkMessage<CheckListPreviewApiModel>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
