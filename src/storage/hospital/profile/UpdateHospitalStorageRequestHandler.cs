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
    public class UpdateHospitalStorageRequestHandler : IRequestHandler<StorageRequest<UpdateHospital>, Hospital>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Hospital> Handle(IReceiveContext<StorageRequest<UpdateHospital>> context, CancellationToken cancellationToken)
        {
            var updated = context.Message.Payload;
            var hospital = _context.Hospital.First(x => x.Id == updated.Id);
            hospital.Name = updated.Name;
            hospital.Remark = updated.Remark;
            hospital.ConsumeDays = updated.ConsumeDays;

            _context.Hospital.Update(hospital);
            await _context.SaveChangesAsync();
            return hospital;
        }
    }
}
