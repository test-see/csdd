using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apitest.store
{
    [TestClass]
    public class StoreApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Store_List_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Store/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<StoreListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<StoreListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task Store_Add_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Store/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CustomizeStoreChangeApiModel
                {
                    HospitalGoodsId = 1,
                    ChangeQty = 1,
                    ChangeTypeId = 1
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.Store>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task HospitalDepartment_Type_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Store/changetype")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataStoreChangeType>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }


        [TestMethod]
        public async Task HospitalGoods_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Store/index")
                .SetQueryParam("goodid","1")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<Store>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data != null);
        }

    }
}
