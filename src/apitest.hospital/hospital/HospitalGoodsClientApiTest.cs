using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apitest.hospital
{
    [TestClass]
    public class HospitalGoodsClientApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task HospitalGoodsClient_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoodsClient/list")
                .WithOAuthBearerToken(await getToken())
                .SetQueryParam("goodsId", "1")
                .GetJsonAsync<OkMessage<IList<HospitalGoodsClientListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Count > 0);
        }

        [TestMethod]
        public async Task HospitalGoodsClient_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalGoodsClient/add")
                .WithOAuthBearerToken(await getToken())
                .SetQueryParam("goodsId", "1")
                .SetQueryParam("clientId", "1")
                .GetJsonAsync<OkMessage<foundation.ef5.poco.HospitalGoodsClient>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/HospitalGoodsClient/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

    }
}