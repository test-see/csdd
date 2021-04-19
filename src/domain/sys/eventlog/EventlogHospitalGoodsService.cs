using domain.eventlog.valueobjects;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net;
using storage.hospitalgoods.carrier;
using System.Threading.Tasks;

namespace domain.eventlog
{
    public class EventlogHospitalGoodsService
    {
        private readonly IMediator _mediator;
        public EventlogHospitalGoodsService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<EventlogHospitalGoods> CreateAsync(CreateEventlogHospitalGoodsRequest created)
        {
            var goods = await _mediator.RequestSingleByIdAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(created.GoodsId);
            created.HospitalGoods = goods;
            return await _mediator.RequestAsync<CreateEventlogHospitalGoodsRequest, EventlogHospitalGoods>(created);
        }
    }

}