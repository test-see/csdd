using Mediator.Net.Contracts;

namespace irespository.hospital.goods.model
{
    public class UpdateHospitalGoodsIsActiveRequest : IRequest
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
