using csdd.Controllers.Shared;
using foundation.config;
using irespository.user.hospital.model;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class UserHospitalController : DefaultControllerBase
    {
        private readonly IUserHospitalService _UserHospitalService;
        public UserHospitalController(IUserHospitalService UserHospitalService)
        {
            _UserHospitalService = UserHospitalService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<UserHospitalListQueryModel> query)
        {
            var data = await _UserHospitalService.GetPagerListAsync(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _UserHospitalService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(UserHospitalCreateApiModel created)
        {
            var data = _UserHospitalService.Create(created, UserId);
            return Json(data);
        }
    }
}
