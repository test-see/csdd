using foundation.ef5.poco;
using storage.adapter.v2.client;
using System.Threading.Tasks;

namespace domain.v2.client
{
    public class ClientService
    {
        private readonly IClientRespository _clientRespository;
        public ClientService(IClientRespository clientRespository)
        {
            _clientRespository = clientRespository;
        }
        public async Task<Client> CreateAsync(ClientCreation payload, int userId)
        {
            return await _clientRespository.CreateAsync(new Client
            {
                CreateUserId = userId,
                Name = payload.Name,
            });
        }
        public async Task<Client> UpdateAsync(ClientUpdation payload)
        {
            return await _clientRespository.UpdateAsync(new Client
            {
                Name = payload.Name,
            });
        }
    }

    public class ClientCreation
    {
        public string Name { get; set; }
    }
    public class ClientUpdation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
