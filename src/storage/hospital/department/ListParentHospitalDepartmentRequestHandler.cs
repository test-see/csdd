﻿using foundation.config;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.EntityFrameworkCore;
using storage.hospital.department.carrier;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.request.client
{
    public class ListParentHospitalDepartmentRequestHandler : IRequestHandler<ListParentHospitalDepartmentRequest, ListResponse<IdNameValueModel>>
    {
        private readonly DefaultDbContext _context;
        public ListParentHospitalDepartmentRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task<ListResponse<IdNameValueModel>> Handle(IReceiveContext<ListParentHospitalDepartmentRequest> context, CancellationToken cancellationToken)
        {
            var data = await _context.HospitalDepartment.Select(x => new IdNameValueModel { Id = x.Id, Name = x.Name }).ToListAsync();

            return data.ToResponse();
        }
    }
}
