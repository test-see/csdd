using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientGoods2HospitalGoodsRequestHandler : IRequestHandler<CreateClientGoods2HospitalGoods, ClientGoods2HospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public CreateClientGoods2HospitalGoodsRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ClientGoods2HospitalGoods> Handle(IReceiveContext<CreateClientGoods2HospitalGoods> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var mapping = new ClientGoods2HospitalGoods
            {
                ClientGoodsId = created.ClientGoodsId,
                ClientQty = created.ClientQty,
                HospitalGoodsId = created.HospitalGoodsId,
                HospitalQty = created.HospitalQty,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };
            _context.ClientGoods2HospitalGoods.Add(mapping);
           await _context.SaveChangesAsync();

            return mapping;
        }
    }
}
