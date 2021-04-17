using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.sys
{
    [TestClass]
    public class ConfigApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Config_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Config/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ListConfigRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListConfigResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Config_AddAndDelete_ReturnIntAsync()
        {
            var role = await _rootpath
                .AppendPathSegment("/api/Config/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new ConfigCreateApiModel { Key = "q", Remark = "d", Value = "d" })
                .ReceiveJson<OkMessage<SysConfig>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Config/{role.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
