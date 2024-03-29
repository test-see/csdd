﻿using apitest.shared;
using domain.client.profile.entity;
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
    public class WhitePhoneApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task WhitePhone_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/WhitePhone/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ListWhitePhoneRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListWhitePhoneResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task WhitePhone_AddAndDelete_ReturnIntAsync()
        {
            var role = await _rootpath
                .AppendPathSegment("/api/WhitePhone/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CreateWhitePhoneRequest { Phone = "1" })
                .ReceiveJson<OkMessage<SysWhitePhone>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/WhitePhone/{role.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
