using domain.client;
using foundation.config;
using irespository.client.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListClientRequestHandler : IRequestHandler<PagerQuery<ListClientRequest>, PagerResult<ListClientResponse>>
    {
        private readonly ClientService _clientContext;
        public ListClientRequestHandler(ClientService clientContext)
        {
            _clientContext = clientContext;
        }

        public Task<PagerResult<ListClientResponse>> Handle(IReceiveContext<PagerQuery<ListClientRequest>> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(_clientContext.GetPagerList(context.Message));
        }
    }
}
