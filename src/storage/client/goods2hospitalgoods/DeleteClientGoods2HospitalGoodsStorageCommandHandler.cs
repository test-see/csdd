using domain.client.goods2hospitalgoods.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteClientGoods2HospitalGoodsStorageCommandHandler : ICommandHandler<StorageCommand<DeleteClientGoods2HospitalGoods>>
    {
        private readonly DefaultDbContext _context;
        public DeleteClientGoods2HospitalGoodsStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteClientGoods2HospitalGoods>> context, CancellationToken cancellationToken)
        {
            var mapping = _context.ClientGoods2HospitalGoods.Find(context.Message.Payload.Id);
            _context.ClientGoods2HospitalGoods.Remove(mapping);
            await _context.SaveChangesAsync();
        }
    }
}
