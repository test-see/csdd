using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user.enums;
using irespository.user.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.sys
{
    [TestClass]
    public class UserApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task User_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<UserListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<UserListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }

        [TestMethod]
        public async Task User_Add_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserCreateApiModel
                {
                    Phone = "+T" + DateTime.UtcNow.ToString("yyMMddmmHHss"),
                    Username = "Test1",
                    RoleIds = new List<int> {3,6,7 }
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task User_InActive_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/1/inactive")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<foundation.ef5.poco.User>>();
            Assert.AreEqual(200, message.Code);
            message = await _rootpath
                .AppendPathSegment("/api/User/1/active")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<foundation.ef5.poco.User>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task User_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task User_Update_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserUpdateApiModel
                {
                    Username = "Test",
                    RoleIds = new List<int> { }
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task User_GetAuthorizeRole_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/authorize")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataPortal>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }

    }
}
