﻿using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using irespository.user.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                .ReceiveJson<OkMessage<PagerResult<foundation.ef5.poco.User>>>();
            Assert.AreEqual(200, message.Code);
        }


        [TestMethod]
        public async Task UserRole_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/role")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<UserRoleListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }


        [TestMethod]
        public async Task UserRole_Update_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/User/role/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserRoleListUpdateModel { UserId = 1, RoleIds = new List<int> { } })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
