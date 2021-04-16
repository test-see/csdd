﻿using foundation.ef5;
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
    public class UpdateHospitalGoodsIsActiveStorageRequestHandler : IRequestHandler<StorageRequest<UpdateHospitalGoodsIsActive>, HospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalGoodsIsActiveStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<StorageRequest<UpdateHospitalGoodsIsActive>> context, CancellationToken cancellationToken)
        {
            var updated = context.Message.Payload;
            var goods = _context.HospitalGoods.First(x => x.Id == updated.Id);

            goods.IsActive = updated.IsActive ? 1 : 0;

            _context.HospitalGoods.Update(goods);
            await _context.SaveChangesAsync();
            return goods;
        }
    }
}
