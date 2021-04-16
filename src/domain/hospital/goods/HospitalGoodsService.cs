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
            return await _mediator.RequestAsync<StorageRequest<CreateHospitalGoods>, HospitalGoods>(new StorageRequest<CreateHospitalGoods>(created));
        }

        public async Task<HospitalGoods> UpdateAsync(UpdateHospitalGoods updated)
        {
            var reponse = await _mediator.RequestAsync<StorageRequest<UpdateHospitalGoods>, HospitalGoods>(new StorageRequest<UpdateHospitalGoods>(updated));
            _eventlogHospitalGoodsContext.Create(new EventlogHospitalGoodsChangeValueModel
            {
                GoodId = reponse.Id,
                ChangeValue = null,
            }, updated.UserId);
            return reponse;
        }

        public async Task DeleteAsync(DeleteHospitalGoods deleted)
        {
            await _mediator.SendAsync(new StorageCommand<DeleteHospitalGoods>(deleted));
        }
        public async Task<HospitalGoods> UpdateIsActiveAsync(UpdateHospitalGoodsIsActive updated)
        {
            var reponse = await _mediator.RequestAsync<StorageRequest<UpdateHospitalGoodsIsActive>, HospitalGoods>(new StorageRequest<UpdateHospitalGoodsIsActive>(updated));
            return reponse;
        }

        public HospitalGoodsValueModel GetValueByBarcode(string barcode)
        {
            return _hospitalGoodsRespository.GetValueByBarcode(barcode);
        }
        public HospitalGoodsIndexApiModel GetIndex(int id)
        {
            var goods = _hospitalGoodsRespository.GetIndex(id);
            goods.Logs = _eventlogHospitalGoodsContext.GetList(id);
            return goods;
        }
    }
}
