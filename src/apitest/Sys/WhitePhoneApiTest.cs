using apitest.Shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apitest.Sys
{
    [TestClass]
    public class WhitePhoneApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task WhitePhone_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/WhitePhone/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<WhitePhoneListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<WhitePhoneListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
