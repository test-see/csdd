using Mediator.Net.Contracts;

namespace irespository.hospital.profile.model
{
    public class GetHospitalResponse : IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumeDays { get; set; }
        public string Remark { get; set; }
    }
}
