﻿using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.client.goods.model;
using irespository.client.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class ClientGoodsApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task ClientGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/ClientGoods/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ClientGoodsListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<ClientGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task ClientGoods_AddAndDelete_ReturnIntAsync()
        {
            var Client = await _rootpath
                .AppendPathSegment("/api/ClientGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new ClientGoodsCreateApiModel
                {
                    Name = "q",
                    ClientId = 1,
                    Producer = "x",
                    UnitPurchase = "x",
                    Spec = "x"
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.ClientGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/ClientGoods/{Client.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task ClientGoods_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/ClientGoods/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new ClientGoodsUpdateApiModel
                {
                    Name = "q",
                    Producer = "x",
                    UnitPurchase = "x",
                    Spec = "x",
                    IsActive = 1,
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

        [TestMethod]
        public async Task ClientGoods_Index_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/ClientGoods/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<ClientGoodsIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}