using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class CreatingClientRequestHandler : IRequestHandler<CreatingClient, Client>
    {
        private readonly DefaultDbContext _context;
        public CreatingClientRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<Client> Handle(IReceiveContext<CreatingClient> context, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                Name = context.Message.Name,
                CreateUserId = context.Message.UserId,
                CreateTime = DateTime.Now,
            };

            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
