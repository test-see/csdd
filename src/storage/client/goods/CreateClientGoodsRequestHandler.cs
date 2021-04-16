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
    public class CreateClientGoodsRequestHandler : IRequestHandler<CreateClientGoods, ClientGoods>
    {
        private readonly DefaultDbContext _context;
        public CreateClientGoodsRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<ClientGoods> Handle(IReceiveContext<CreateClientGoods> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var goods = new ClientGoods
            {
                Code = created.Code,
                Name = created.Name,
                ClientId = created.ClientId,
                Producer = created.Producer,
                Spec = created.Spec,
                Unit = created.Unit,
                CreateUserId = created.UserId,
                IsActive = 1,
                CreateTime = DateTime.Now,
            };

            _context.ClientGoods.Add(goods);
            await _context.SaveChangesAsync();
            return goods;
        }
    }
}
