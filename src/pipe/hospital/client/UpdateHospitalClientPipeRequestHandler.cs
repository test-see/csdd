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
    public class UpdateHospitalClientPipeRequestHandler : IRequestHandler<Pipe<UpdateHospitalClient>, HospitalClient>
    {
        private readonly HospitalClientService _service;
        public UpdateHospitalClientPipeRequestHandler(HospitalClientService service)
        {
            _service = service;
        }
        public async Task<HospitalClient> Handle(IReceiveContext<Pipe<UpdateHospitalClient>> context, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(context.Message.Payload);
        }
    }
}
