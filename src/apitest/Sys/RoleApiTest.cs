using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.Sys
{
    [TestClass]
    public class RoleApiTest: BaseApiTest
    {
        [TestMethod]
        public async Task Role_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Role/list")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<string>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
