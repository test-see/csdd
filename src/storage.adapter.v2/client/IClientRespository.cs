using foundation.ef5.poco;
using System.Threading.Tasks;

namespace storage.adapter.v2.client
{
    public interface IClientRespository
    {
         Task<ClientOverview> GetOverviewByIdAsync(int id);
    }
}
