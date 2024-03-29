﻿using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apitest.shared
{
    [TestClass]
    public class ProfileApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task User_Profile_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Profile/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }
        [TestMethod]
        public async Task User_Profile_Menus_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Profile/menu/list")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IList<string>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

    }
}
