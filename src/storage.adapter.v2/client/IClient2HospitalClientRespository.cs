using System.Threading.Tasks;

namespace storage.adapter.v2.client
{
    public interface IClient2HospitalClientRespository
    {
        Task DeleteAllByClientIdAsync(int clientId);
        Task DeleteAsync(int id);
    }
}
