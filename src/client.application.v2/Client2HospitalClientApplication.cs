using domain.v2.client;
using foundation.config;
using storage.qurable.v2.client;
using System;
using System.Linq;
using System.Threading.Tasks;
using static domain.v2.client.Client2HospitalClientService;
using static storage.qurable.v2.client.IClient2HospitalClientQurableRespository;

namespace client.application.v2
{
    public class Client2HospitalClientApplication
    {
        private readonly ClientService _clientService;
        private readonly IClient2HospitalClientQurableRespository _client2HospitalClientQurableRespository;
        public Client2HospitalClientApplication(ClientService clientService,
            IClient2HospitalClientQurableRespository client2HospitalClientQurableRespository)
        {
            _clientService = clientService;
            _client2HospitalClientQurableRespository = client2HospitalClientQurableRespository;
        }
        public async Task DeleteAsync(int id)
        {
            await _clientService.HospitalClientService.DeleteAsync(id);
        }
        public async Task<int> CreateAsync(Client2HospitalClientCreation payload, int userId)
        {
            var data = await _clientService.HospitalClientService.CreateAsync(payload, userId);
            return data.Id;
        }
        public PagerResult<GetClient2HospitalClient> GetOverviewByPage(PagerQuery<Client2HospitalClientQurable> payload)
        {
            var data = _client2HospitalClientQurableRespository.ListOverviewByPage(payload);
            return new PagerResult<GetClient2HospitalClient>
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
            };
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
}
