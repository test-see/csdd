﻿using foundation.ef5;
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
    public class CreateHospitalGoodsStorageRequestHandler : IRequestHandler<StorageRequest<CreateHospitalGoods>, HospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public CreateHospitalGoodsStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalGoods> Handle(IReceiveContext<StorageRequest<CreateHospitalGoods>> context, CancellationToken cancellationToken)
        {
            var created = context.Message.Payload;
            var goods = new HospitalGoods
            {
                Code = created.Code,
                Name = created.Name,
                HospitalId = created.HospitalId,
                Producer = created.Producer,
                Spec = created.Spec,
                Unit = created.Unit,
                CreateUserId = created.UserId,
                IsActive = 1,
                PinShou = created.PinShou,
                Barcode = created.Barcode,
                Price = created.Price,
                CreateTime = DateTime.Now,
            };

            _context.HospitalGoods.Add(goods);
            await _context.SaveChangesAsync();

            return goods;
        }
    }
}
