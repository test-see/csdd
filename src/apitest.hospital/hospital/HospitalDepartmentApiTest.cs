using apitest.shared;
using Flurl;
using Flurl.Http;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;
using irespository.hospital.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using storage.hospitaldepartment.carrier;
using System.Collections.Generic;
using System.Linq;
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
                .PostJsonAsync(new PagerQuery<ListHospitalDepartmentRequest> { })
                .ReceiveJson<OkMessage<PagerResult<ListHospitalDepartmentResponse>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Total > 0);
        }
        [TestMethod]
        public async Task HospitalDepartment_AddAndDelete_ReturnIntAsync()
        {
            var hospital = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/add")
                .WithOAuthBearerToken(await getToken())
                .PostJsonAsync(new CreateHospitalDepartmentRequest
                {
                    Name = "q",
                    HospitalId = 1,
                    DepartmentTypeId = 1,
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
                .PostJsonAsync(new UpdateHospitalDepartmentRequest
                {
                    Name = "q",
                    DepartmentTypeId = 1,
                    ParentId = 0,
                })
                .ReceiveJson<OkMessage<int>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data > 0);
        }

        [TestMethod]
        public async Task HospitalDepartment_Type_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/type")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<DataDepartmentType>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }

        [TestMethod]
        public async Task HospitalDepartment_Parent_ReturnListAsync()
        {
            var message = await _rootpath
                .AppendPathSegment("/api/HospitalDepartment/parent")
                .WithOAuthBearerToken(await getToken())
                .GetJsonAsync<OkMessage<IEnumerable<IdNameValueModel>>>();
            Assert.AreEqual(200, message.Code);
            Assert.IsTrue(message.Data.Any());
        }
    }
}