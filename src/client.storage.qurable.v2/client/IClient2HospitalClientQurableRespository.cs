using foundation.config;
using foundation.ef5.poco;

namespace storage.qurable.v2.client
{
    public interface IClient2HospitalClientQurableRespository
    {
        PagerResult<Client2HospitalClientOverview> ListOverviewByPage(PagerQuery<Client2HospitalClientQurable> payload);
    }

    public class Client2HospitalClientOverview
    {
        public User User { get; set; }
        public HospitalClient HospitalClient { get; set; }
        public Hospital Hospital { get; set; }
        public Client Client { get; set; }
        public Client2HospitalClient Mapping { get; set; }

    }

    public class Client2HospitalClientQurable
    {
        public int? HospitalId { get; set; }
        public string Name { get; set; }
        public int? ClientId { get; set; }
    }
}
