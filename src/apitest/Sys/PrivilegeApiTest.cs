using apitest.Shared;
using domain.sys.entities;
using Flurl;
using Flurl.Http;
using foundation.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
                .AppendPathSegment("/api/Privilege/menu/list")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<MenuEntity>>>();
            Assert.AreEqual(200, message.Code);
        }
    }
}
