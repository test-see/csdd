using client.storage.qurable.v2.data;
using foundation.config;

namespace storage.qurable.v2.client
{
    public interface IClient2HospitalClientQurableRespository
    {
        PagerResult<Client2HospitalClientOverview> GetOverviewByPage(PagerQuery<Client2HospitalClientQurable> payload);
    

    }

}
