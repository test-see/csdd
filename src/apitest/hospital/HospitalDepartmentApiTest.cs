using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using irespository.hospital.department.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace apitest.hospital
{
    [TestClass]
    public class HospitalDepartmentApiTest : BaseApiTest
    {
        [TestMethod]
        public async Task HospitalDepartment_Post_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/list")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new PagerQuery<HospitalDepartmentListQueryModel> { })
                .ReceiveJson<OkMessage<PagerResult<HospitalDepartmentListApiModel>>>();
            Assert.AreEqual(200, message.Code);
        }
        [TestMethod]
        public async Task HospitalDepartment_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalDepartmentCreateApiModel
                {
                    Name = "q",
                    HospitalId = 1,
                })
                .ReceiveJson<OkMessage<foundation.ef5.poco.HospitalDepartment>>();
            var message = await _rootpath
                .AppendPathSegment($"/api/HospitalDepartment/{hospital.Data.Id}/delete")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }
        [TestMethod]
        public async Task HospitalDepartment_Update_ReturnIntAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/1/update")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new HospitalDepartmentUpdateApiModel
                {
                    Name = "q",
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }


    }
}