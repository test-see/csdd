using domain.client.profile.entity;
using domain.hospital;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalPipeCommandHandler : ICommandHandler<Pipe<DeleteHospitalCommand>>
    {
        private readonly HospitalService _service;
        public DeleteHospitalPipeCommandHandler(HospitalService service)
        {
            _service = service;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteHospitalCommand>> context, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(context.Message.Payload);
        }
    }
}
