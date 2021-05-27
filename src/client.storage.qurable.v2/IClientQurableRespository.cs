using foundation.config;
using foundation.ef5.poco;
using System.Threading.Tasks;

namespace storage.qurable.v2.client
{
    public interface IClientQurableRespository
    {
        Task<ClientOverview> GetOverviewByIdAsync(int id);
        PagerResult<ClientOverview> GetOverviewByPage(PagerQuery<ClientQurable> payload);
        public class ClientOverview
        {
            public Client Client { get; set; }
            public User User { get; set; }
        }
        public class ClientQurable
        {
            public string Name { get; set; }
        }
    }
}
