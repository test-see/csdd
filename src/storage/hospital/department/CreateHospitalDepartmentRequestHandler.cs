using foundation.ef5;
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
    public class CreateHospitalDepartmentRequestHandler : IRequestHandler<CreateHospitalDepartmentRequest, HospitalDepartment>
    {
        private readonly DefaultDbContext _context;
        public CreateHospitalDepartmentRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalDepartment> Handle(IReceiveContext<CreateHospitalDepartmentRequest> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var goods = new HospitalDepartment
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                DepartmentTypeId = created.DepartmentTypeId,
                CreateUserId = created.UserId,
                ParentId = created.ParentId,
                CreateTime = DateTime.Now,
                IsPurchaseCheck = created.IsPurchaseCheck,
            };

            _context.HospitalDepartment.Add(goods);
            await _context.SaveChangesAsync();

            return goods;
        }
    }
}
