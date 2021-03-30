using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.model;
using System;
using System.Linq;
using System.Collections.Generic;
using irespository.hospital.client.model;
using irespository.hospital.goods.model;

namespace respository.hospital
{
    public class HospitalGoodsClientRespository : IHospitalGoodsClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        public HospitalGoodsClientRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository,
            IHospitalClientRespository hospitalClientRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _hospitalClientRespository = hospitalClientRespository;
        }
        public HospitalGoodsClient Create(int goodsId, int clientId, int userId)
        {
            var goods = new HospitalGoodsClient
            {
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                HospitalClientId = clientId,
                HospitalGoodsId = goodsId,
            };

            _context.HospitalGoodsClient.Add(goods);
            _context.SaveChanges();

            return goods;
        }

        public int Delete(int id)
        {
            var goods = _context.HospitalGoodsClient.Find(id);
            _context.HospitalGoodsClient.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public IList<HospitalGoodsClientListApiModel> GetListByGoodsId(int goodsId)
        {
            var sql = from r in _context.HospitalGoodsClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.HospitalGoodsId == goodsId
                      select new HospitalGoodsClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          HospitalClient = new HospitalClientValueModel { Id = r.HospitalClientId },
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId },
                      };
            var result = sql.ToList();
            var clients = _hospitalClientRespository.GetValue(result.Select(x => x.HospitalClient.Id).ToArray());
            var goods = _hospitalGoodsRespository.GetValue(result.Select(x => x.HospitalGoods.Id).ToArray());
            foreach (var m in result)
            {
                m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return result;
        }

        public PagerResult<HospitalGoodsClientListApiModel> GetPagerList(PagerQuery<HospitalGoodsClientQueryModel> query)
        {
            var sql = from r in _context.HospitalGoodsClient
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new HospitalGoodsClientListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CreateUserName = u.Username,
                          HospitalClient = new HospitalClientValueModel { Id = r.HospitalClientId },
                          HospitalGoods = new HospitalGoodsValueModel { Id = r.HospitalGoodsId },
                      };
            if (query.Query?.HospitalClientId != null)
            {
                sql = sql.Where(x => x.HospitalClient.Id == query.Query.HospitalClientId.Value);
            }
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => x.HospitalGoods.Id == query.Query.HospitalGoodsId.Value);
            }
            var data = new PagerResult<HospitalGoodsClientListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var clients = _hospitalClientRespository.GetValue(data.Result.Select(x => x.HospitalClient.Id).ToArray());
                var goods = _hospitalGoodsRespository.GetValue(data.Result.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in data.Result)
                {
                    m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
            }
            return data;
        }
    }
}
