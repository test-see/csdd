using csdd.Controllers.Shared;
using domain.client.profile.entity;
using foundation.config;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.sys.model;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Sys
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ConfigController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public ConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListConfigRequest> query)
        {
            var data = await _mediator.RequestPagerListAsync<ListConfigRequest, ListConfigResponse>(query);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.SendPipeAsync(new DeleteConfigCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateConfigRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.RequestPipeAsync<CreateConfigRequest, SysConfig>(created);
            return Json(data);
        }

    }
}
