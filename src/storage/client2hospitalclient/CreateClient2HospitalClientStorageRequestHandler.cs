using domain.client.profile.entity;
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
    public class CreateClient2HospitalClientStorageRequestHandler : IRequestHandler<StorageRequest<CreateClient2HospitalClient>, Client2HospitalClient>
    {
        private readonly DefaultDbContext _context;
        public CreateClient2HospitalClientStorageRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client2HospitalClient> Handle(IReceiveContext<StorageRequest<CreateClient2HospitalClient>> context, CancellationToken cancellationToken)
        {
            var created = context.Message.Payload;
            var mapping = new Client2HospitalClient
            {
                HospitalClientId = created.HospitalClientId,
                ClientId = created.ClientId,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };
            _context.Client2HospitalClient.Add(mapping);
            _context.SaveChanges();

            return mapping;
        }
    }
}
