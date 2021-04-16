using domain.hospital;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateHospitalDepartmentPipeRequestHandler : IRequestHandler<Pipe<CreateHospitalDepartment>, HospitalDepartment>
    {
        private readonly HospitalDepartmentService _service;
        public CreateHospitalDepartmentPipeRequestHandler(HospitalDepartmentService service)
        {
            _service = service;
        }
        public async Task<HospitalDepartment> Handle(IReceiveContext<Pipe<CreateHospitalDepartment>> context, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(context.Message.Payload);
        }
    }
}
