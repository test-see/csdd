using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateClientRequestHandler : IRequestHandler<UpdateClientRequest, Client>
    {
        private readonly DefaultDbContext _context;
        public UpdateClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client> Handle(IReceiveContext<UpdateClientRequest> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var client = _context.Client.First(x => x.Id == payload.Id);
            client.Name = payload.Name;

            _context.Client.Update(client);
            await _context.SaveChangesAsync();

            return client;
        }
    }
}
