using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.goods.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateHospitalGoodsClientRequestHandler : IRequestHandler<CreateHospitalGoodsClientRequest, HospitalGoodsClient>
    {
        private readonly DefaultDbContext _context;
        public CreateHospitalGoodsClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalGoodsClient> Handle(IReceiveContext<CreateHospitalGoodsClientRequest> context, CancellationToken cancellationToken)
        {
            var created = context.Message; 
            var goods = new HospitalGoodsClient
            {
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
                HospitalClientId = created.HospitalClientId,
                HospitalGoodsId = created.HospitalGoodsId,
            };

            _context.HospitalGoodsClient.Add(goods);
            await _context.SaveChangesAsync();

            return goods;
        }
    }
}
