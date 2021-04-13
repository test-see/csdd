using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
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
                .ReceiveJson<OkMessage<PagerResult<ListHospitalGoodsResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
    }
}