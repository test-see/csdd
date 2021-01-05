﻿using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.sys
{
    [TestClass]
    public class HospitalGoodsApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task HospitalGoods_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalGoodsListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<HospitalGoodsListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task HospitalGoods_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalGoodsCreateApiModel { Name = "q", HospitalId = 1, Producer = "x", UnitPurchase = "x", Spec = "x" })
                .ReceiveJson<OkMessage<foundation.ef5.poco.HospitalGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/HospitalGoods/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task HospitalGoods_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalGoodsUpdateApiModel { Id = 1, Name = "q", Producer = "x", UnitPurchase = "x", Spec = "x" })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}