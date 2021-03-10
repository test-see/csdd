using irespository.client.profile.model;
using irespository.hospital.client.model;
using System;

namespace irespository.client.maping.model
{
    public class ClientMappingListApiModel
    {
        public int ClientMappingId { get; set; }
        public HospitalClientValueModel HospitalClient { get; set; }
        public ClientValueModel Client { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
