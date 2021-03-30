using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.storeinout;
using irespository.storeinout.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.store
{
    public class StoreInoutGoodsRespository : IStoreInoutGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public StoreInoutGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }
        public PagerResult<StoreInoutGoodsListApiModel> GetPagerList(PagerQuery<StoreInoutGoodsListQueryModel> query)
        {
            var sql = from r in _context.StoreInoutGoods
                      orderby r.Id descending
                      select new StoreInoutGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          StoreInoutId = r.StoreInoutId,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            if (query.Query?.StoreInoutId != null)
            {
                sql = sql.Where(x => x.StoreInoutId == query.Query.StoreInoutId.Value);
            }
            var data = new PagerResult<StoreInoutGoodsListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
            }
            return data;
        }
        public IList<StoreInoutGoodsListApiModel> GetListByStoreInout(int storeInoutId)
        {
            var sql = from r in _context.StoreInoutGoods
                      where r.StoreInoutId == storeInoutId
                      select new StoreInoutGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Qty = r.Qty,
                          StoreInoutId = r.StoreInoutId,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            var data = sql.ToList();
            var goods = _hospitalGoodsRespository.GetValue(data.Select(x => x.HospitalGoods.Id).ToArray());
            foreach (var m in data)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return data;
        }

        public StoreInoutGoods Create(StoreInoutGoodsCreateApiModel created, int userId)
        {
            var setting = new StoreInoutGoods
            {
                StoreInoutId = created.StoreInoutId,
                HospitalGoodsId = created.HospitalGoodsId,
                Qty = created.Qty,
                CreateTime = DateTime.Now,
            };

            _context.StoreInoutGoods.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var setting = _context.StoreInoutGoods.Find(id);
            _context.StoreInoutGoods.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, StoreInoutGoodsUpdateApiModel updated)
        {
            var setting = _context.StoreInoutGoods.First(x => x.Id == id);
            setting.Qty = updated.Qty;

            _context.StoreInoutGoods.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
