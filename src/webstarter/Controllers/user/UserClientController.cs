using csdd.Controllers.Shared;
using foundation.config;
using irespository.user.client.model;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class UserClientController : DefaultControllerBase
    {
        private readonly IUserClientService _UserClientService;
        public UserClientController(IUserClientService UserClientService)
        {
            _UserClientService = UserClientService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<UserClientListQueryModel> query)
        {
            var data = _UserClientService.GetPagerListAsync(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _UserClientService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(UserClientCreateApiModel created)
        {
            var data = _UserClientService.Create(created, UserId);
            return Json(data);
        }
    }
}
