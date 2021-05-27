using csdd.Controllers.Shared;
using domain.v2.client;
using foundation.config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using storage.qurable.v2.client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace csdd.controllers.client
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("api/ClientMapping")]
    public class Client2HospitalClientController : DefaultControllerBase
    {
        private readonly ClientService _clientService;
        private readonly IClient2HospitalClientQurableRespository _client2HospitalClientQurableRespository;
        public Client2HospitalClientController(ClientService clientService,
            IClient2HospitalClientQurableRespository client2HospitalClientQurableRespository)
        {
            _clientService = clientService;
            _client2HospitalClientQurableRespository = client2HospitalClientQurableRespository;
        }
        [HttpGet]
        [Route("{id}/delete")]
        public async Task<OkMessage<int>> DeleteAsync(int id)
        {
            await _clientService.HospitalClientService.DeleteAsync(id);
            return OkMessage(id);
        }
        [HttpPost]
        [Route("add")]
        public async Task<OkMessage<int>> PostAsync(Client2HospitalClientCreation payload)
        {
            var data = await _clientService.HospitalClientService.CreateAsync(payload, UserId);
            return OkMessage(data.Id);
        }
        [HttpPost]
        [Route("list")]
        public OkMessage<PagerResult<GetClient2HospitalClient>> List(PagerQuery<Client2HospitalClientQurable> payload)
        {
            var data = _client2HospitalClientQurableRespository.ListOverviewByPage(payload);
            return OkMessage(new PagerResult<GetClient2HospitalClient>
            {
                Index = data.Index,
                Size = data.Size,
                Total = data.Total,
                Result = data.Result.Select(x => new GetClient2HospitalClient
                {
                    ClientMappingId = x.Mapping.Id,
                    CreateTime = x.Mapping.CreateTime,
                    CreateUserName = x.User.Username,
                    HospitalClient = new GetHospitalClient
                    {
                        Id = x.HospitalClient.Id,
                        Name = x.HospitalClient.Name,
                        Hospital = new GetHospital
                        {
                            Id = x.Hospital.Id,
                            Name = x.Hospital.Name,
                            ConsumeDays = x.Hospital.ConsumeDays,
                            Remark = x.Hospital.Remark,
                        },
                    },
                    Client = new GetClient
                    {
                        Id = x.Client.Id,
                        Name = x.Client.Name,
                    }
                })
            });
        }
    }
    public class GetClient2HospitalClient
    {
        public int ClientMappingId { get; set; }
        public GetHospitalClient HospitalClient { get; set; }
        public GetClient Client { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
    public class GetHospitalClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetHospital Hospital { get; set; }
    }
    public class GetClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetHospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumeDays { get; set; }
        public string Remark { get; set; }
    }
}
