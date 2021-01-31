using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.store.profile.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.hospital.store
{
    [TestClass]
    public class StoreRecordApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task StoreRecord_List_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreRecord/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<StoreRecordListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<StoreRecordListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
    }
}
