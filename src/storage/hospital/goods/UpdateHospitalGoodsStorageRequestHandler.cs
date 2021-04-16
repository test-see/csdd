using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateHospitalGoodsStorageRequestHandler : IRequestHandler<StorageRequest<UpdateHospitalGoods>, HospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalGoodsStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<StorageRequest<UpdateHospitalGoods>> context, CancellationToken cancellationToken)
        {
            var updated = context.Message.Payload;
            var goods = _context.HospitalGoods.First(x => x.Id == updated.Id);

            goods.Code = updated.Code;
            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.Unit = updated.Unit;
            goods.PinShou = updated.PinShou;
            goods.Price = updated.Price;
            goods.Barcode = updated.Barcode;

            _context.HospitalGoods.Update(goods);
            await _context.SaveChangesAsync();
            return goods;
        }
    }
}
