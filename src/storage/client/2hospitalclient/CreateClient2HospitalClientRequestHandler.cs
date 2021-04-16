using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.client.maping.model;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClient2HospitalClientRequestHandler : IRequestHandler<CreateClient2HospitalClient, Client2HospitalClient>
    {
        private readonly DefaultDbContext _context;
        public CreateClient2HospitalClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client2HospitalClient> Handle(IReceiveContext<CreateClient2HospitalClient> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var mapping = new Client2HospitalClient
            {
                HospitalClientId = created.HospitalClientId,
                ClientId = created.ClientId,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };
            _context.Client2HospitalClient.Add(mapping);
            await _context.SaveChangesAsync();

            return mapping;
        }
    }
}
