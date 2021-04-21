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
    public class WhitePhoneController : DefaultControllerBase
    {
        private readonly IMediator _mediator;
        public WhitePhoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> ListAsync(PagerQuery<ListWhitePhoneRequest> query)
        {
            var data = await _mediator.ListByPageAsync<ListWhitePhoneRequest, ListWhitePhoneResponse>(query);
            return Json(data);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            await _mediator.ToPipeAsync(new DeleteWhitePhoneCommand { Id = id });
            return Json(id);
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> PostAsync(CreateWhitePhoneRequest created)
        {
            created.UserId = UserId;
            var data = await _mediator.ToPipeAsync<CreateWhitePhoneRequest, SysWhitePhone>(created);
            return Json(data);
        }

    }
}
