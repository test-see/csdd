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
    public class UpdateHospitalGoodsIsActiveRequestHandler : IRequestHandler<UpdateHospitalGoodsIsActive, HospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalGoodsIsActiveRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<UpdateHospitalGoodsIsActive> context, CancellationToken cancellationToken)
        {
            var updated = context.Message;
            var goods = _context.HospitalGoods.First(x => x.Id == updated.Id);

            goods.IsActive = updated.IsActive ? 1 : 0;

            _context.HospitalGoods.Update(goods);
            await _context.SaveChangesAsync();
            return goods;
        }
    }
}
