using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apitest.Sys
{
    [TestClass]
    public class PrivilegeApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Menu_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Privilege/list")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<PrivilegeListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
