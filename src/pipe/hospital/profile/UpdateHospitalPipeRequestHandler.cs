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
    public class UpdateHospitalPipeRequestHandler : IRequestHandler<Pipe<UpdateHospital>, Hospital>
    {
        private readonly HospitalService _service;
        public UpdateHospitalPipeRequestHandler(HospitalService service)
        {
            _service = service;
        }
        public async Task<Hospital> Handle(IReceiveContext<Pipe<UpdateHospital>> context, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(context.Message.Payload);
        }
    }
}
