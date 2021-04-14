﻿using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateHospitalStorageRequestHandler : IRequestHandler<StorageRequest<CreateHospital>, Hospital>
    {
        private readonly DefaultDbContext _context;
        public CreateHospitalStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Hospital> Handle(IReceiveContext<StorageRequest<CreateHospital>> context, CancellationToken cancellationToken)
        {
            var created = context.Message.Payload;
            var hospital = new Hospital
            {
                Name = created.Name,
                Remark = created.Remark,
                ConsumeDays = created.ConsumeDays,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.Hospital.Add(hospital);
            await _context.SaveChangesAsync();

            return hospital;
        }
    }
}
