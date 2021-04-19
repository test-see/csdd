using Mediator.Net.Contracts;

namespace irespository.hospital.goods.model
{
    public class ListHospitalGoodsClientRequest:IRequest
    {
        public int? HospitalClientId { get; set; }
        public int? HospitalGoodsId { get; set; }
    }
}
