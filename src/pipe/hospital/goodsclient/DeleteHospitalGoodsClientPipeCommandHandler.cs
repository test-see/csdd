using domain.client.profile.entity;
using domain.hospital;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalGoodsClientPipeCommandHandler : ICommandHandler<Pipe<DeleteHospitalGoodsClientCommand>>
    {
        private readonly HospitalGoodsClientService _service;
        public DeleteHospitalGoodsClientPipeCommandHandler(HospitalGoodsClientService service)
        {
            _service = service;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteHospitalGoodsClientCommand>> context, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(context.Message.Payload);
        }
    }
}
