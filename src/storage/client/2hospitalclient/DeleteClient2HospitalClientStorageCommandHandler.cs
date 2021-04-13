using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClient2HospitalClientStorageCommandHandler : ICommandHandler<StorageCommand<DeleteClient2HospitalClient>>
    {
        private readonly DefaultDbContext _context;
        public DeleteClient2HospitalClientStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteClient2HospitalClient>> context, CancellationToken cancellationToken)
        {
            var mapping = _context.Client2HospitalClient.Find(context.Message.Payload.Id);
            _context.Client2HospitalClient.Remove(mapping);
            await _context.SaveChangesAsync();
        }
    }
}
