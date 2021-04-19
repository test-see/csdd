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
    public class UpdateHospitalClientRequestHandler : IRequestHandler<UpdateHospitalClientRequest, HospitalClient>
    {
        private readonly DefaultDbContext _context;
        public UpdateHospitalClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalClient> Handle(IReceiveContext<UpdateHospitalClientRequest> context, CancellationToken cancellationToken)
        {
            var updated = context.Message;
            var goods = _context.HospitalClient.First(x => x.Id == updated.Id);

            goods.Name = updated.Name;

            _context.HospitalClient.Update(goods);
            await _context.SaveChangesAsync();
            return goods;
        }
    }
}
