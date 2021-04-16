using domain.client.profile.entity;
using domain.hospital;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalClientPipeCommandHandler : ICommandHandler<Pipe<DeleteHospitalClient>>
    {
        private readonly HospitalClientService _service;
        public DeleteHospitalClientPipeCommandHandler(HospitalClientService service)
        {
            _service = service;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteHospitalClient>> context, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(context.Message.Payload);
        }
    }
}
