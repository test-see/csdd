using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using irespository.user.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apitest.User
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
        public async Task UserRole_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }


        [TestMethod]
        public async Task UserRole_Update_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserUpdateApiModel { UserId = 1, RoleIds = new List<int> { } })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
