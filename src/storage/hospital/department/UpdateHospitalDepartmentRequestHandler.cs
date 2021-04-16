using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class UpdateHospitalDepartmentRequestHandler : IRequestHandler<UpdateHospitalDepartment, HospitalDepartment>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalDepartmentRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalDepartment> Handle(IReceiveContext<UpdateHospitalDepartment> context, CancellationToken cancellationToken)
        {
            var updated = context.Message;
            var department = _context.HospitalDepartment.First(x => x.Id == updated.Id);

            department.Name = updated.Name;
            department.DepartmentTypeId = updated.DepartmentTypeId;
            department.ParentId = updated.ParentId;
            department.IsPurchaseCheck = updated.IsPurchaseCheck;

            _context.HospitalDepartment.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }
}
