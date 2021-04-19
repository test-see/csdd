using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalClientCommandHandler : ICommandHandler<DeleteHospitalClientCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalClientCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteHospitalClientCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Id;
            var goods = _context.HospitalClient.Find(id);
            _context.HospitalClient.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
