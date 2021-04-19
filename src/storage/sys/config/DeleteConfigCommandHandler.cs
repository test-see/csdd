using domain.client.profile.entity;
using foundation.ef5;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.config
{
    public class DeleteConfigCommandHandler : ICommandHandler<DeleteConfigCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteConfigCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteConfigCommand> context, CancellationToken cancellationToken)
        {
            var config = _context.SysConfig.Find(context.Message.Id);
            _context.SysConfig.Remove(config);
            await _context.SaveChangesAsync();
        }
    }
}
