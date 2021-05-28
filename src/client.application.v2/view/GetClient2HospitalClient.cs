using System;

namespace client.application.v2.view
{
    public class GetClient2HospitalClient
    {
        public int ClientMappingId { get; set; }
        public GetHospitalClient HospitalClient { get; set; }
        public GetClient Client { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public class GetHospitalClient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public GetHospital Hospital { get; set; }
        }
        public class GetClient
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class GetHospital
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ConsumeDays { get; set; }
            public string Remark { get; set; }
        }
    }
}
