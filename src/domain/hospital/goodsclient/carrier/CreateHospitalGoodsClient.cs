using Mediator.Net.Contracts;

namespace irespository.hospital.goods.model
{
    public class CreateHospitalGoodsClient : IRequest
    {
        public int UserId { get; set; }
        public int HospitalClientId { get; set; }
        public int HospitalGoodsId { get; set; }
    }
}
