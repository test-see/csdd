using Mediator.Net.Contracts;

namespace irespository.hospital.goods.model
{
    public class UpdateHospitalGoodsIsActive : IRequest
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
