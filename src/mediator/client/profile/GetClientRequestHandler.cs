using domain.client;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using nouns.client.profile;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client.profile
{
    public class GetClientRequestHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly ClientContext _clientContext;
        public GetClientRequestHandler(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public Task<GetClientResponse> Handle(IReceiveContext<GetClientRequest> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(_clientContext.GetIndex(context.Message.Id));
        }
    }
}
