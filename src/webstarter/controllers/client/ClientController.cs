using csdd.Controllers.Shared;
using domain.v2.client;
using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using storage.qurable.v2.client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace csdd.Controllers.Info
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ClientController : DefaultControllerBase
    {
        private readonly IClientQurableRespository _clientRespository;
        private readonly ClientService _clientService;
        public ClientController(IClientQurableRespository clientRespository,
            ClientService clientService)
        {
            _clientRespository = clientRespository;
            _clientService = clientService;
        }

        [HttpGet]
        [Route("{id}/index")]
        public async Task<OkMessage<GetClient>> GetAsync(int id)
        {
            var data = await _clientRespository.GetOverviewByIdAsync(id);
            return OkMessage(new GetClient
            {
                CreateTime = data.Client.CreateTime,
                CreateUserName = data.User.Username,
                Id = data.Client.Id,
                Name = data.Client.Name,
            });
        }
        [HttpPost]
        [Route("list")]
        public OkMessage<PagerResult<GetClient>> List(PagerQuery<ClientQurable> payload)
        {
            var data = _clientRespository.ListOverviewByPage(payload);
            return OkMessage(new PagerResult<GetClient>
            {
                Index = data.Index,
                Size = data.Size,
                Total = data.Total,
                Result = data.Result.Select(x => new GetClient
                {
                    CreateTime = x.Client.CreateTime,
                    CreateUserName = x.User.Username,
                    Id = x.Client.Id,
                    Name = x.Client.Name,
                }).ToList(),
            });
        }
        [HttpPost]
        [Route("add")]
        public async Task<OkMessage<int>> PostAsync(ClientCreation payload)
        {
            var data = await _clientService.CreateAsync(payload, UserId);
            return OkMessage(data.Id);
        }
        [HttpPost]
        [Route("{id}/update")]
        public async Task<OkMessage<int>> UpdateAsync(int id, ClientUpdation payload)
        {
            payload.Id = id;
            var data = await _clientService.UpdateAsync(payload);
            return OkMessage(data.Id);
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<OkMessage<int>> DeleteAsync(int id)
        {
            await _clientService.DeleteAsync(id);
            return OkMessage(id);
        }
    }
    public class GetClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
