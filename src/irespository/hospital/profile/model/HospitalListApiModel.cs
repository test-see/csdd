using System;

namespace irespository.hospital.model
{
    public class HospitalListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public string Remark { get; set; }
    }
}
