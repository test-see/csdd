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
    }
}
