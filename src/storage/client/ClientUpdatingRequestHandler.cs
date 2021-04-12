using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class ClientUpdatingRequestHandler : IRequestHandler<ClientUpdating, Client>
    {
        private readonly DefaultDbContext _context;
        public ClientUpdatingRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client> Handle(IReceiveContext<ClientUpdating> context, CancellationToken cancellationToken)
        {
            var client = _context.Client.First(x => x.Id == context.Message.Id);
            client.Name = context.Message.Name;

            _context.Client.Update(client);
            await _context.SaveChangesAsync();

            return client;
        }
    }
}
