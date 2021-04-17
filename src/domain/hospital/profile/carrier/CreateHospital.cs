using Mediator.Net.Contracts;

namespace irespository.hospital.model
{
    public class CreateHospital : IRequest
    {
        public string Name { get; set; }
        public int ConsumeDays { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
    }
}
