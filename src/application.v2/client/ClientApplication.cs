using domain.v2.client;
using foundation.ef5.poco;
using System.Threading.Tasks;

namespace application.v2.client
{
    public class ClientApplication
    {
        private readonly ClientService _clientService;
        public ClientApplication(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<Client> CreateAsync(ClientCreation payload, int userId)
        {
            return await _clientService.CreateAsync(payload, userId);
        }
    }
}
