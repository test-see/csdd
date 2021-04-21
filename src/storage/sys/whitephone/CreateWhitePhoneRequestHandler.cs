using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.WhitePhone
{
    public class CreateWhitePhoneRequestHandler : IRequestHandler<CreateWhitePhoneRequest, SysWhitePhone>
    {
        private readonly DefaultDbContext _context;
        public CreateWhitePhoneRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<SysWhitePhone> Handle(IReceiveContext<CreateWhitePhoneRequest> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var phone = new SysWhitePhone
            {
                Phone = created.Phone,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.SysWhitePhone.Add(phone);
            await _context.SaveChangesAsync();
            return phone;
        }
    }
}
