using domain.hospital;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateHospitalGoodsPipeRequestHandler : IRequestHandler<Pipe<UpdateHospitalGoods>, HospitalGoods>
    {
        private readonly HospitalGoodsService _service;
        public UpdateHospitalGoodsPipeRequestHandler(HospitalGoodsService service)
        {
            _service = service;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<Pipe<UpdateHospitalGoods>> context, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(context.Message.Payload);
        }
    }
}
