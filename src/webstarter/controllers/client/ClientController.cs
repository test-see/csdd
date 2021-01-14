using csdd.Controllers.Shared;
using foundation.config;
using irespository.client.model;
using iservice.client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IClientService _ClientService;
        public ClientController(IClientService ClientService)
        {
            _ClientService = ClientService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<ClientListQueryModel> query)
        {
            var data = _ClientService.GetPagerList(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _ClientService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(ClientCreateApiModel created)
        {
            var data = _ClientService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, ClientUpdateApiModel updated)
        {
            var data = _ClientService.Update(id, updated);
            return Json(data);
        }
    }
}
