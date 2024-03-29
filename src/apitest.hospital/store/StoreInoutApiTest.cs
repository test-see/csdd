﻿using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.storeinout
{
    [TestClass]
    public class StoreInoutTest : BaseApiTest
    {
        [TestMethod]
        public async Task StoreInout_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<StoreInoutListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<StoreInoutListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task StoreInout_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/StoreInout/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutCreateApiModel { Name = "1", Remark = "2", ChangeTypeId=2, })
                .ReceiveJson<OkMessage<foundation.ef5.poco.StoreInout>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/StoreInout/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task StoreInout_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new StoreInoutUpdateApiModel { Name = "1", Remark = "2", Id = 1 })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

        [TestMethod]
        public async Task StoreInout_Submit_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/1/submit")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task StoreInout_Type_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/changetype")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataStoreChangeType>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }
        [TestMethod]
        public async Task StoreInout_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/StoreInout/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<StoreInoutIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

    }
}
