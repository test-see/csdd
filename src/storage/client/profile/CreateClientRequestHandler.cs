using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreateClientRequestHandler : IRequestHandler<CreateClient, Client>
    {
        private readonly DefaultDbContext _context;
        public CreateClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client> Handle(IReceiveContext<CreateClient> context, CancellationToken cancellationToken)
        {
            var payload = context.Message;
            var client = new Client
            {
                Name = payload.Name,
                CreateUserId = payload.UserId,
                CreateTime = DateTime.Now,
            };

            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
