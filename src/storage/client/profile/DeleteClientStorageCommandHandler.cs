using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientStorageCommandHandler : ICommandHandler<StorageCommand<DeleteClient>>
    {
        private readonly DefaultDbContext _context;
        public DeleteClientStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteClient>> context, CancellationToken cancellationToken)
        {
            var mappings = _context.Client2HospitalClient.Where(x => x.ClientId == context.Message.Payload.Id);
            _context.Client2HospitalClient.RemoveRange(mappings);

            var Client = _context.Client.Find(context.Message.Payload.Id);
            _context.Client.Remove(Client);
            await _context.SaveChangesAsync();
        }
    }
}
