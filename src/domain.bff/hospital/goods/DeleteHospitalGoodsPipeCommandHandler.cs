using domain.client.profile.entity;
using domain.hospital;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalGoodsPipeCommandHandler : ICommandHandler<Pipe<DeleteHospitalGoodsCommand>>
    {
        private readonly HospitalGoodsService _service;
        public DeleteHospitalGoodsPipeCommandHandler(HospitalGoodsService service)
        {
            _service = service;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteHospitalGoodsCommand>> context, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(context.Message.Payload);
        }
    }
}
