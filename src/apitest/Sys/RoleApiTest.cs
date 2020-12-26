using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
                .AppendPathSegment("/api/Role/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new RoleCreateApiModel { RoleName = "1" })
                .ReceiveJson<OkMessage<SysRole>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/Role/{role.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

        [TestMethod]
        public async Task Privilege_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Role/2/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<RoleIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }


        [TestMethod]
        public async Task Privilege_Update_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Role/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new RoleIndexUpdateModel { RoleId = 2, RoleName = "Test", MenuIds = new List<int> { 1 } })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
