﻿using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.client.maping.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class ClientMappingApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task ClientMapping_AddAndDelete_ReturnIntAsync()
        {
            var Client = await _rootpath
                .AppendPathSegment("/api/ClientMapping/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new ClientMappingCreateApiModel { ClientId = 1, HospitalClientId = 1 })
                .ReceiveJson<OkMessage<foundation.ef5.poco.ClientMapping>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/ClientMapping/{Client.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
