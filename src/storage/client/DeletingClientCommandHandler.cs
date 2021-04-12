using domain.client.profile.entity;
using foundation.ef5;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeletingClientCommandHandler : ICommandHandler<DeletingClient>
    {
        private readonly DefaultDbContext _context;
        public DeletingClientCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeletingClient> context, CancellationToken cancellationToken)
        {
            var mappings = _context.Client2HospitalClient.Where(x => x.ClientId == context.Message.Id);
            _context.Client2HospitalClient.RemoveRange(mappings);

            var Client = _context.Client.Find(context.Message.Id);
            _context.Client.Remove(Client);
            await _context.SaveChangesAsync();
        }
    }
}
