using foundation.ef5.poco;
using storage.qurable.v2.client;
using System.Threading.Tasks;

namespace storage.adapter.v2.client
{
    public interface IClientRespository: IClientQurableRespository
    {
        Task<Client> CreateAsync(Client payload);
    }
}
