using irespository.client.profile.model;
using irespository.hospital.profile.model;

namespace irespository.hospital.client.model
{
    public class HospitalClientValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalValueModel Hospital { get; set; }
        public ClientValueModel Client { get; set; }
    }
}
