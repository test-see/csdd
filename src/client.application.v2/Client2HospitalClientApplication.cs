using client.application.v2.view;
using client.domain.v2.entity;
using client.storage.qurable.v2.data;
using domain.v2.client;
using foundation.config;
using storage.qurable.v2.client;
using System.Linq;
using System.Threading.Tasks;

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
        public PagerResult<GetClient2HospitalClient> GetByPage(PagerQuery<Client2HospitalClientQurable> payload)
        {
            var data = _client2HospitalClientQurableRespository.GetOverviewByPage(payload);
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
                    HospitalClient = new GetClient2HospitalClient.GetHospitalClient
                    {
                        Id = x.HospitalClient.Id,
                        Name = x.HospitalClient.Name,
                        Hospital = new GetClient2HospitalClient.GetHospital
                        {
                            Id = x.Hospital.Id,
                            Name = x.Hospital.Name,
                            ConsumeDays = x.Hospital.ConsumeDays,
                            Remark = x.Hospital.Remark,
                        },
                    },
                    Client = new GetClient2HospitalClient.GetClient
                    {
                        Id = x.Client.Id,
                        Name = x.Client.Name,
                    }
                })
            };
        }
    }
}
