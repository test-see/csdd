using foundation.ef5.poco;
using storage.adapter.v2.client;
using System.Threading.Tasks;

namespace domain.v2.client
{
    public class Client2HospitalClientService
    {
        private readonly IClient2HospitalClientRespository _client2HospitalClientRespository;
        public Client2HospitalClientService(IClient2HospitalClientRespository client2HospitalClientRespository)
        {
            _client2HospitalClientRespository = client2HospitalClientRespository;
        }
        public async Task DeleteAllByClientIdAsync(int clientId)
        {
            await _client2HospitalClientRespository.DeleteAllByClientIdAsync(clientId);
        }
        public async Task DeleteAsync(int id)
        {
            await _client2HospitalClientRespository.DeleteAsync(id);
        }
        public async Task<Client2HospitalClient> CreateAsync(Client2HospitalClientCreation payload, int userId)
        {
            return await _client2HospitalClientRespository.CreateAsync(new Client2HospitalClient
            {
                CreateUserId = userId,
                ClientId = payload.ClientId,
                HospitalClientId = payload.HospitalClientId,
            });
        }
    }
    public class Client2HospitalClientCreation
    {
        public int ClientId { get; set; }
        public int HospitalClientId { get; set; }
    }
}
