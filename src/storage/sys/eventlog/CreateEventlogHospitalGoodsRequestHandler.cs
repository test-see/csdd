using domain.eventlog.valueobjects;
using foundation.ef5;
using foundation.ef5.poco;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace storage.sys.eventlog
{
    public class CreateEventlogHospitalGoodsRequestHandler : IRequestHandler<CreateEventlogHospitalGoods, EventlogHospitalGoods>
    {
        private readonly DefaultDbContext _context;
        public CreateEventlogHospitalGoodsRequestHandler(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<EventlogHospitalGoods> Handle(IReceiveContext<CreateEventlogHospitalGoods> context, CancellationToken cancellationToken)
        {
            var created = context.Message;
            var log = new Eventlog
            {
                Content = created.ChangeValue == null ?
                    "没有记录细节" :
                    $"修改药品信息 {created.ChangeValue.Field} 从 {created.ChangeValue.Source} 改为 {created.ChangeValue.Result}",
                Title = $"修改药品 {created.HospitalGoods.Name}",
                OptionUserId = created.UserId,
                CreateTime = DateTime.Now,
            };

            _context.Eventlog.Add(log);
            await _context.SaveChangesAsync();

            var reference = new EventlogHospitalGoods
            {
                HospitalGoodsId = created.GoodsId,
                EventlogId = log.Id,
            };
            _context.EventlogHospitalGoods.Add(reference);

            await _context.SaveChangesAsync();
            return reference;
        }
    }
}
