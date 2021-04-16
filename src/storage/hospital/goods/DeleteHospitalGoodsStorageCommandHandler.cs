using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalGoodsStorageCommandHandler : ICommandHandler<StorageCommand<DeleteHospitalGoods>>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalGoodsStorageCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<StorageCommand<DeleteHospitalGoods>> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Payload;
            var goods = _context.HospitalGoods.Find(id);
            _context.HospitalGoods.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
