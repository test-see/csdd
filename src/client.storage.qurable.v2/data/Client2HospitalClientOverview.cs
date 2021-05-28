using foundation.ef5.poco;

namespace client.storage.qurable.v2.data
{
    public class Client2HospitalClientOverview
    {
        public User User { get; set; }
        public HospitalClient HospitalClient { get; set; }
        public Hospital Hospital { get; set; }
        public Client Client { get; set; }
        public Client2HospitalClient Mapping { get; set; }

    }
}
