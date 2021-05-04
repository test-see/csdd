using domain.client;
using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.maping.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClient2HospitalClientPipeRequestHandler : IRequestHandler<Pipe<CreateClient2HospitalClientRequest>, Client2HospitalClient>
    {
        private readonly Client2HospitalClientService _clientContext;
        public CreateClient2HospitalClientPipeRequestHandler(Client2HospitalClientService clientContext)
        {
            _clientContext = clientContext;
        }
        public async Task<Client2HospitalClient> Handle(IReceiveContext<Pipe<CreateClient2HospitalClientRequest>> context, CancellationToken cancellationToken)
        {
            return await _clientContext.CreateAsync(context.Message.Payload);
        }
    }
}
