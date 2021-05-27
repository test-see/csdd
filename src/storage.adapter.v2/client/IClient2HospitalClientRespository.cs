using foundation.ef5.poco;
using System.Threading.Tasks;

namespace storage.adapter.v2.client
{
    public interface IClient2HospitalClientRespository
    {
        Task DeleteAllByClientIdAsync(int clientId);
        Task DeleteAsync(int id);
        Task<Client2HospitalClient> CreateAsync(Client2HospitalClient payload);
    }
}
