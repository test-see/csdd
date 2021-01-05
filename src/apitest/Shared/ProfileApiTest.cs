using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.sys.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.shared
{
    [TestClass]
    public class ProfileApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task User_Profile_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Profile/index")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<UserIndexApiModel>>();
            Assert.AreEqual(200, message.Code);
        }

    }
}
