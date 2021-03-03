using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.model;
using System;
using System.Linq;
using System.Collections.Generic;
using irespository.hospital.client.model;

namespace respository.hospital
{
    public class HospitalGoodsClientRespository : IHospitalGoodsClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        public HospitalGoodsClientRespository(DefaultDbContext context,
            IHospitalClientRespository hospitalClientRespository)
        {
            _context = context;
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

        public IList<HospitalGoodsClientListApiModel> GeListByGoodsId(int goodsId)
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
                      };
            var result = sql.ToList();
            var clients =  _hospitalClientRespository.GetValue(result.Select(x => x.HospitalClient.Id).ToArray());
            foreach (var m in result)
            {
                m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
            }
            return result;
        }
    }
}
