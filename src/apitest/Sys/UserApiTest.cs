using apitest.Shared;
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
using System.Threading.Tasks;

namespace apitest.Sys
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
                    AuthorizeRoleId = (int)AuthorizeRole.Admin,
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
        public async Task User_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task User_Profile_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/profile")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }

        [TestMethod]
        public async Task User_Update_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserUpdateApiModel { UserId = 1, Username = "Test", RoleIds = new List<int> { } })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
        
        [TestMethod]
        public async Task UserAuthorizeRole_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/data/authorize")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataAuthorizeRole>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
