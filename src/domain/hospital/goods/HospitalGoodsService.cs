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
        public async Task<HospitalGoods> CreateAsync(CreateHospitalGoods created)
        {
            return await _mediator.RequestSingleAsync<CreateHospitalGoods, HospitalGoods>(created);
        }

        public async Task<HospitalGoods> UpdateAsync(UpdateHospitalGoods updated)
        {
            var response = await _mediator.RequestSingleAsync<UpdateHospitalGoods, HospitalGoods>(updated);
            _eventlogHospitalGoodsContext.Create(new EventlogHospitalGoodsChangeValueModel
            {
                GoodId = response.Id,
                ChangeValue = null,
            }, updated.UserId);
            return response;
        }

        public async Task DeleteAsync(DeleteHospitalGoods deleted)
        {
            await _mediator.SendStorageAsync(deleted);
        }
        public async Task<HospitalGoods> UpdateIsActiveAsync(UpdateHospitalGoodsIsActive updated)
        {
            return await _mediator.RequestSingleAsync<UpdateHospitalGoodsIsActive, HospitalGoods>(updated);
        }
    }
}
