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
        public async Task<HospitalGoodsClient> CreateAsync(CreateHospitalGoodsClient created)
        {
            return await _mediator.RequestSingleAsync<CreateHospitalGoodsClient, HospitalGoodsClient>(created);
        }
        public async Task DeleteAsync(DeleteHospitalGoodsClient deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
