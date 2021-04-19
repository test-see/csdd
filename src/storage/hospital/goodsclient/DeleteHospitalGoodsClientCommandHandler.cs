using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalGoodsClientCommandHandler : ICommandHandler<DeleteHospitalGoodsClientCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalGoodsClientCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteHospitalGoodsClientCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message;
            var goods = _context.HospitalGoodsClient.Find(id);
            _context.HospitalGoodsClient.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
