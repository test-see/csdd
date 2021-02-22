using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using System;
using System.Collections.Generic;

namespace respository.hospital
{
    public class HospitalGoodsClientRespository : IHospitalGoodsClientRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalGoodsClientRespository(DefaultDbContext context)
        {
            _context = context;
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

        public IList<IdNameValueModel> GeClientList(int goodsId)
        {
            throw new NotImplementedException();
        }
    }
}
