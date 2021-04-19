using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalCommandHandler : ICommandHandler<DeleteHospitalCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteHospitalCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Id;
            var hospital = _context.Hospital.Find(id);
            _context.Hospital.Remove(hospital);
            await _context.SaveChangesAsync();
        }
    }
}
