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
    public class UpdateHospitalDepartmentPipeRequestHandler : IRequestHandler<Pipe<UpdateHospitalDepartment>, HospitalDepartment>
    {
        private readonly HospitalDepartmentService _service;
        public UpdateHospitalDepartmentPipeRequestHandler(HospitalDepartmentService service)
        {
            _service = service;
        }
        public async Task<HospitalDepartment> Handle(IReceiveContext<Pipe<UpdateHospitalDepartment>> context, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(context.Message.Payload);
        }
    }
}
