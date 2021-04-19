using domain.client.profile.entity;
using domain.eventlog;
using domain.eventlog.valueobjects;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalGoodsService
    {
        private readonly IMediator _mediator;
        private readonly EventlogHospitalGoodsService _eventlogHospitalGoodsContext;
        public HospitalGoodsService(IMediator mediator,
            EventlogHospitalGoodsService eventlogHospitalGoodsContext)
        {
            _mediator = mediator;
            _eventlogHospitalGoodsContext = eventlogHospitalGoodsContext;
        }
        public async Task<HospitalGoods> CreateAsync(CreateHospitalGoodsRequest created)
        {
            return await _mediator.RequestSingleAsync<CreateHospitalGoodsRequest, HospitalGoods>(created);
        }

        public async Task<HospitalGoods> UpdateAsync(UpdateHospitalGoodsRequest updated)
        {
            var response = await _mediator.RequestSingleAsync<UpdateHospitalGoodsRequest, HospitalGoods>(updated);
            await _eventlogHospitalGoodsContext.CreateAsync(new CreateEventlogHospitalGoodsRequest
            {
                GoodsId = response.Id,
                UserId = updated.UserId,
            });
            return response;
        }

        public async Task DeleteAsync(DeleteHospitalGoodsCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
        public async Task<HospitalGoods> UpdateIsActiveAsync(UpdateHospitalGoodsIsActiveRequest updated)
        {
            return await _mediator.RequestSingleAsync<UpdateHospitalGoodsIsActiveRequest, HospitalGoods>(updated);
        }
    }
}
