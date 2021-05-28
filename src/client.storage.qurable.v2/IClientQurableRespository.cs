using client.storage.qurable.v2.data;
using foundation.config;
using System.Threading.Tasks;

namespace storage.qurable.v2.client
{
    public interface IClientQurableRespository
    {
        Task<ClientOverview> GetOverviewByIdAsync(int id);
        PagerResult<ClientOverview> GetOverviewByPage(PagerQuery<ClientQurable> payload);
        
    }
}
