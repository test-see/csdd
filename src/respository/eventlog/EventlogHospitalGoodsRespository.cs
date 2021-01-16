using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.eventlog;
using irespository.eventlog.model;
using irespository.sys.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.eventlog
{
    public class EventlogHospitalGoodsRespository: IEventlogHospitalGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public EventlogHospitalGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public EventlogHospitalGoods Create(EventlogHospitalGoodsCreateApiModel created, int userId)
        {
            var log = new Eventlog
            {
                Content = created.Content,
                Title = created.Title,
                OptionUserId = userId,
            };

            _context.Eventlog.Add(log);
            _context.SaveChanges();

            var reference = new EventlogHospitalGoods
            {
                HospitalGoodsId = created.GoodsId,
                EventlogId = log.Id,
            };
            _context.EventlogHospitalGoods.Add(reference);

            _context.SaveChanges();
            return reference;
        }
        public IList<EventlogListApiModel> GetList(int goodsId)
        {
            var sql = from g in _context.EventlogHospitalGoods
                      join r in _context.Eventlog on g.EventlogId equals r.Id
                      join u in _context.User on r.OptionUserId equals u.Id
                      where g.HospitalGoodsId == goodsId
                      select new EventlogListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Content = r.Content,
                          OptionUsername = u.Username,
                          Title = r.Title,
                      };
            return sql.ToList();
        }

    }
}
