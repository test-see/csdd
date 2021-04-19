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
                .PostJsonAsync(new PagerQuery<ListHospitalGoodsRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListHospitalGoodsResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task HospitalGoods_Store_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/store/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<ListHospitalGoodsRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListHospitalGoodsStoreResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }

        [TestMethod]
        public async Task HospitalGoods_Query_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/query/index")
                .WithOAuthBearerToken(await getToken())
                .SetQueryParam("barcode", "1")
                .GetJsonAsync<OkMessage<GetHospitalGoodsResponse>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task HospitalGoods_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CreateHospitalGoodsRequest
                {
                    Name = "q",
                    HospitalId = 1,
                    Producer = "x",
                    Unit = "x",
                    Spec = "x",
                    PinShou = "x",
                    Price = new decimal(1.11),
                })
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
                .PostJsonAsync(new UpdateHospitalGoodsRequest
                {
                    Name = "q",
                    Producer = "x",
                    Unit = "x",
                    Spec = "x",
                    IsActive = 1,
                    PinShou = "x",
                    Price = new decimal(1.11),
                    Barcode = "1",
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


        [TestMethod]
        public async Task HospitalGoods_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/1/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<GetHospitalGoodsResponse>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

        [TestMethod]
        public async Task HospitalGoods_InActive_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/1/inactive")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<foundation.ef5.poco.HospitalGoods>>();
            Assert.AreEqual(200, message.Code);
            message = await _rootpath
                .AppendPathSegment("/api/HospitalGoods/1/active")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<foundation.ef5.poco.HospitalGoods>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}