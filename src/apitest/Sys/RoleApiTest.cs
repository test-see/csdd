using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.Sys
{
    [TestClass]
    public class RoleApiTest: BaseApiTest
    {
        [TestMethod]
        public async Task Role_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Role/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<RoleListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<RoleListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task Role_AddAndDelete_ReturnIntAsync()
        {
            var role = await _rootpath
                .AppendPathSegment("/api/Role")
                .WithOAuthBearerToken(await getToken())
                .SetQueryParam("name", "test")
                .PostAsync()
                .ReceiveJson<OkMessage<SysRole>>();
            var message = await _rootpath
                .AppendPathSegment("/api/Role/delete")
                .SetQueryParam("id", role.Data.Id)
                .WithOAuthBearerToken(await getToken())
                .PostAsync()
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
