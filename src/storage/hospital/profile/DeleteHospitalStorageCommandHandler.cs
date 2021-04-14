using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalStorageCommandHandler : ICommandHandler<StorageCommand<DeleteHospital>>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteHospital>> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Payload;
            var hospital = _context.Hospital.Find(id);
            _context.Hospital.Remove(hospital);
            await _context.SaveChangesAsync();
        }
    }
}
