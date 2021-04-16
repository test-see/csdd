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
    public class CreateHospitalGoodsClientPipeRequestHandler : IRequestHandler<Pipe<CreateHospitalGoodsClient>, HospitalGoodsClient>
    {
        private readonly HospitalGoodsClientService _service;
        public CreateHospitalGoodsClientPipeRequestHandler(HospitalGoodsClientService service)
        {
            _service = service;
        }
        public async Task<HospitalGoodsClient> Handle(IReceiveContext<Pipe<CreateHospitalGoodsClient>> context, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(context.Message.Payload);
        }
    }
}
