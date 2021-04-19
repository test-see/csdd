using domain.client.profile.entity;
using foundation.ef5;
using foundation.mediator;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.client
{
    public class DeleteHospitalDepartmentCommandHandler : ICommandHandler<DeleteHospitalDepartmentCommand>
    {
        private readonly DefaultDbContext _context;
        public DeleteHospitalDepartmentCommandHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task Handle(IReceiveContext<DeleteHospitalDepartmentCommand> context, CancellationToken cancellationToken)
        {
            var id = context.Message.Id;
            var goods = _context.HospitalDepartment.Find(id);
            _context.HospitalDepartment.Remove(goods);
            await _context.SaveChangesAsync();
        }
    }
}
