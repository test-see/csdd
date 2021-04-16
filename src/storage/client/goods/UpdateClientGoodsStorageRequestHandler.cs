using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateClientGoodsStorageRequestHandler : IRequestHandler<StorageRequest<UpdateClientGoods>, ClientGoods>
    {
        private readonly DefaultDbContext _context;
        public UpdateClientGoodsStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ClientGoods> Handle(IReceiveContext<StorageRequest<UpdateClientGoods>> context, CancellationToken cancellationToken)
        {
            var updated = context.Message.Payload;
            var goods = _context.ClientGoods.First(x => x.Id == updated.Id);

            goods.Code = updated.Code;
            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.Unit = updated.Unit;
            goods.IsActive = updated.IsActive;

            _context.ClientGoods.Update(goods);
            await _context.SaveChangesAsync();

            return goods;
        }
    }
}
