using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalGoodsClientService
    {
        private readonly IMediator _mediator;
        public HospitalGoodsClientService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<HospitalGoodsClient> CreateAsync(CreateHospitalGoodsClientRequest created)
        {
            return await _mediator.RequestAsync<CreateHospitalGoodsClientRequest, HospitalGoodsClient>(created);
        }
        public async Task DeleteAsync(DeleteHospitalGoodsClientCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
