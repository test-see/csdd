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
    public class CreateHospitalClientRequestHandler : IRequestHandler<CreateHospitalClientRequest, HospitalClient>
    {
        private readonly DefaultDbContext _context;
        public CreateHospitalClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<HospitalClient> Handle(IReceiveContext<CreateHospitalClientRequest> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var goods = new HospitalClient
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.HospitalClient.Add(goods);
            await _context.SaveChangesAsync();

            return goods;
        }
    }
}
