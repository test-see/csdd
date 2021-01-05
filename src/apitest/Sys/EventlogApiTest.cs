using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.sys
{
    [TestClass]
    public class EventlogApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Eventlog_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Eventlog/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<EventlogListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<EventlogListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
