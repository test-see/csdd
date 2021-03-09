using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.client
{
    [TestClass]
    public class HospitalApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task Hospital_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/Hospital/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<HospitalListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
    }
}
