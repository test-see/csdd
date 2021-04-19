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
    public class DeleteClientGoodsCommandHandler : ICommandHandler<DeleteClientGoodsCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteClientGoodsCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteClientGoodsCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Id;
            var mappings = _context.ClientGoods2HospitalGoods.Where(x => x.ClientGoodsId == id);
            _context.ClientGoods2HospitalGoods.RemoveRange(mappings);

            var goods = _context.ClientGoods.Find(id);
            _context.ClientGoods.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
