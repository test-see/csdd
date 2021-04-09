using csdd.Controllers.Shared;
using foundation.config;
using irespository.client.model;
using iservice.client;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMediator _mediator;
        public ClientController(IClientService clientService,
            IMediator mediator)
        {
            _clientService = clientService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<ListClientRequest> query)
        {
            var data = await _mediator.RequestAsync<PagerQuery<ListClientRequest>, PagerResult<ListClientResponse>>(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _clientService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(ClientCreateApiModel created)
        {
            var data = _clientService.Create(created, UserId);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, ClientUpdateApiModel updated)
        {
            var data = _clientService.Update(id, updated, UserId);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/index")]
        public JsonResult GetIndex(int id)
        {
            var data = _clientService.GetIndex(id);
            return Json(data);
        }

    }
}
