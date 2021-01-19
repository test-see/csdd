using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apitest.data
{
    [TestClass]
    public class DataApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task UserAuthorizeRole_Get_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/data/authorize")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataAuthorizeRole>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
