using foundation.ef5;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace nouns.client.profile
{
    public class DeleteClientCommandRespository : ICommandHandler<DeleteClientEntity>
    {
        private readonly DefaultDbContext _context;
        public DeleteClientCommandRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteClientEntity> context, CancellationToken cancellationToken)
        {
            var mappings = _context.Client2HospitalClient.Where(x => x.ClientId == context.Message.Id);
            _context.Client2HospitalClient.RemoveRange(mappings);

            var Client = _context.Client.Find(context.Message.Id);
            _context.Client.Remove(Client);
            await _context.SaveChangesAsync();
        }
    }
}
