using irespository.hospital.goods.model;
using Mediator.Net.Contracts;

namespace domain.eventlog.valueobjects
{
    public class CreateEventlogHospitalGoods:IRequest
    {
        public int GoodsId { get; set; }
        public EventlogChangeValueModel ChangeValue { get; set; }
        public int UserId { get; set; }
        public GetHospitalGoodsResponse HospitalGoods { get; set; }
    }
}
