using domain.client.profile.entity;
using foundation.ef5;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.WhitePhone
{
    public class DeleteWhitePhoneCommandHandler : ICommandHandler<DeleteWhitePhoneCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteWhitePhoneCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteWhitePhoneCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Id;
            var phone = _context.SysWhitePhone.Find(id);
            _context.SysWhitePhone.Remove(phone);
            await _context.SaveChangesAsync();
        }
    }
}
