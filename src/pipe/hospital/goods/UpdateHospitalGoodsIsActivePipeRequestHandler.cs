using domain.hospital;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateHospitalGoodsIsActivePipeRequestHandler : IRequestHandler<Pipe<UpdateHospitalGoodsIsActive>, HospitalGoods>
    {
        private readonly HospitalGoodsService _service;
        public UpdateHospitalGoodsIsActivePipeRequestHandler(HospitalGoodsService service)
        {
            _service = service;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<Pipe<UpdateHospitalGoodsIsActive>> context, CancellationToken cancellationToken)
        {
            return await _service.UpdateIsActiveAsync(context.Message.Payload);
        }
    }
}
