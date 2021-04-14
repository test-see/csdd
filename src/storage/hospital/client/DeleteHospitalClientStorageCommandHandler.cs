using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalClientStorageCommandHandler : ICommandHandler<StorageCommand<DeleteHospitalClient>>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalClientStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteHospitalClient>> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Payload;
            var goods = _context.HospitalClient.Find(id);
            _context.HospitalClient.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
