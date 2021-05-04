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
    public class CreateHospitalGoodsPipeRequestHandler : IRequestHandler<Pipe<CreateHospitalGoodsRequest>, HospitalGoods>
    {
        private readonly HospitalGoodsService _service;
        public CreateHospitalGoodsPipeRequestHandler(HospitalGoodsService service)
        {
            _service = service;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<Pipe<CreateHospitalGoodsRequest>> context, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(context.Message.Payload);
        }
    }
}
