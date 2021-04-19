using domain.client.profile.entity;
using foundation.ef5;
using foundation.ef5.poco;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace mediator.Config
{
    public class CreateConfigRequestHandler : IRequestHandler<CreateConfigRequest, SysConfig>
    {
        private readonly DefaultDbContext _context;
        public CreateConfigRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<SysConfig> Handle(IReceiveContext<CreateConfigRequest> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var config = new SysConfig
            {
                Value = created.Value,
                Key = created.Key,
                Remark = created.Remark,
                CreateUserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.SysConfig.Add(config);
            await _context.SaveChangesAsync();
            return config;
        }
    }
}
