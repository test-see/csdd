using domain.v2.client;
using foundation.config;
using foundation.ef5.poco;
using storage.qurable.v2.client;
using System;
using System.Linq;
using System.Threading.Tasks;
using static domain.v2.client.ClientService;
using static storage.qurable.v2.client.IClientQurableRespository;

namespace client.application.v2
{
    public class ClientApplication
    {
        private readonly ClientService _clientService;
        private readonly IClientQurableRespository _clientRespository;
        public Client2HospitalClientApplication HospitalClientApplication { get; private set; }
        public ClientApplication(IClientQurableRespository clientRespository,
            ClientService clientService,
            Client2HospitalClientApplication hospitalClientApplication)
        {
            _clientService = clientService;
            _clientRespository = clientRespository;
            HospitalClientApplication = hospitalClientApplication;
        }
        public async Task<Client> CreateAsync(ClientCreation payload, int userId)
        {
            return await _clientService.CreateAsync(payload, userId);
        }
        public async Task<Client> UpdateAsync(ClientUpdation payload)
        {
            return await _clientService.UpdateAsync(payload);
        }
        public async Task DeleteAsync(int id)
        {
            await _clientService.DeleteAsync(id);
        }
        public async Task<GetClient> GetOverviewByIdAsync(int id)
        {
            var data = await _clientRespository.GetOverviewByIdAsync(id);
            return new GetClient
            {
                CreateTime = data.Client.CreateTime,
                CreateUserName = data.User.Username,
                Id = data.Client.Id,
                Name = data.Client.Name,
            };
        }
        public PagerResult<GetClient> GetOverviewByPage(PagerQuery<ClientQurable> payload)
        {
            var data = _clientRespository.ListOverviewByPage(payload);
            return new PagerResult<GetClient>
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
            };
        }

        public class GetClient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreateTime { get; set; }
            public string CreateUserName { get; set; }
        }
    }
}
