using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.client.goods.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class ClientMappingGoodsApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task ClientMappingGoods_AddAndDelete_ReturnIntAsync()
        {
            var Client = await _rootpath
                .AppendPathSegment("/api/ClientMappingGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new ClientMappingGoodsCreateApiModel
                {
                    ClientGoodsId = 1,
                    ClientQty = 1,
                    HospitalGoodsId = 1,
                    HospitalQty = 1
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.ClientMappingGoods>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/ClientMappingGoods/{Client.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
