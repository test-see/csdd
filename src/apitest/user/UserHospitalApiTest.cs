using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.user.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.user
{
    [TestClass]
    public class UserHospitalApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task UserHospital_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/UserHospital/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<UserHospitalListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<UserHospitalListApiModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task UserHospital_AddAndDelete_ReturnIntAsync()
        {
            var UserHospital = await _rootpath
                .AppendPathSegment("/api/UserHospital/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new UserHospitalCreateApiModel { Name = "q", HospitalDepartmentId = 1, UserId = 1 })
                .ReceiveJson<OkMessage<foundation.ef5.poco.UserHospital>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/UserHospital/{UserHospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
    }
}
