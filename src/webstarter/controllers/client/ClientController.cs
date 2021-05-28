using client.application.v2;
using client.application.v2.view;
using client.domain.v2.entity;
using client.storage.qurable.v2.data;
using csdd.Controllers.Shared;
using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly ClientApplication _clientApplication;
        public ClientController(ClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<OkMessage<GetClient>> GetAsync(int id)
        {
            var data = await _clientApplication.GetByIdAsync(id);
            return OkMessage(data);
        }
        [HttpPost]
        [Route("list")]
        public OkMessage<PagerResult<GetClient>> GetByPage(PagerQuery<ClientQurable> payload)
        {
            var data = _clientApplication.GetByPage(payload);
            return OkMessage(data);
        }
        [HttpPost]
        [Route("add")]
        public async Task<OkMessage<int>> PostAsync(ClientCreation payload)
        {
            var data = await _clientApplication.CreateAsync(payload, UserId);
            return OkMessage(data.Id);
        }
        [HttpPost]
        [Route("{id}/update")]
        public async Task<OkMessage<int>> UpdateAsync(int id, ClientUpdation payload)
        {
            payload.Id = id;
            var data = await _clientApplication.UpdateAsync(payload);
            return OkMessage(data.Id);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<OkMessage<int>> DeleteAsync(int id)
        {
            await _clientApplication.DeleteAsync(id);
            return OkMessage(id);
        }
    }
}
