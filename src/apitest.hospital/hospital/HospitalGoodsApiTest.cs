using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.hospital
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
            Assert.IsTrue(message.Data.Total > 0);
        }

        [TestMethod]
        public async Task HospitalGoods_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/index")
                .WithOAuthBearerToken(await getToken())
                .SetQueryParam("barcode", "1")
                .GetJsonAsync<OkMessage<HospitalGoodsValueModel>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

    }
}