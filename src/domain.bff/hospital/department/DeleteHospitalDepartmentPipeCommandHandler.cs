using domain.client.profile.entity;
using domain.hospital;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalDepartmentPipeCommandHandler : ICommandHandler<Pipe<DeleteHospitalDepartmentCommand>>
    {
        private readonly HospitalDepartmentService _service;
        public DeleteHospitalDepartmentPipeCommandHandler(HospitalDepartmentService service)
        {
            _service = service;
        }
        public async Task Handle(IReceiveContext<Pipe<DeleteHospitalDepartmentCommand>> context, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(context.Message.Payload);
        }
    }
}
