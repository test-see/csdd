using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.model;
using irespository.client.profile.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientGoodsRespository : IClientGoodsRespository
    {
        private readonly DefaultDbContext _context;
        public ClientGoodsRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public PagerResult<ClientGoodsListApiModel> GetPagerList(PagerQuery<ClientGoodsListQueryModel> query)
        {
            var sql = from r in _context.ClientGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Client on r.ClientId equals h.Id
                      select new ClientGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Client = new ClientValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          UnitPurchase = r.UnitPurchase,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                      };
            return new PagerResult<ClientGoodsListApiModel>(query.Index, query.Size, sql);
        }

        public ClientGoods Create(ClientGoodsCreateApiModel created, int userId)
        {
            var goods = new ClientGoods
            {
                Name = created.Name,
                ClientId = created.ClientId,
                Producer = created.Producer,
                Spec = created.Spec,
                UnitPurchase = created.UnitPurchase,
                CreateUserId = userId,
                IsActive = 1,
            };

            _context.ClientGoods.Add(goods);
            _context.SaveChanges();

            return goods;
        }

        public int Delete(int id)
        {
            var mappings = _context.ClientMappingGoods.Where(x => x.ClientGoodsId == id);
            _context.ClientMappingGoods.RemoveRange(mappings);

            var goods = _context.ClientGoods.Find(id);
            _context.ClientGoods.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, ClientGoodsUpdateApiModel updated, int userId)
        {
            var goods = _context.ClientGoods.First(x => x.Id == id);

            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.UnitPurchase = updated.UnitPurchase;
            goods.IsActive = updated.IsActive;

            _context.ClientGoods.Update(goods);
            _context.SaveChanges();


            return goods.Id;
        }

        public ClientGoodsIndexApiModel GetIndex(int id)
        {
            var sql = from r in _context.ClientGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Client on r.ClientId equals h.Id
                      where r.Id == id
                      select new ClientGoodsIndexApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          Client = new ClientValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          UnitPurchase = r.UnitPurchase,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,

                      };

            var profile = sql.FirstOrDefault();
            profile.Mappings = (from m in _context.ClientMappingGoods
                                join g in _context.HospitalGoods on m.HospitalGoodsId equals g.Id
                                join h in _context.Hospital on g.HospitalId equals h.Id
                                select new KeyValuePair<int, ClientMappingGoodsListApiModel>(m.Id, new ClientMappingGoodsListApiModel
                                {

                                    ClientQty = m.ClientQty,
                                    HospitalQty = m.HospitalQty,
                                    HospitalGoods = new HospitalGoodsValueModel
                                    {
                                        Id = g.Id,
                                        Name = g.Name,
                                        Producer = g.Producer,
                                        Spec = g.Spec,
                                        UnitPurchase = g.UnitPurchase,
                                        Hospital = new HospitalValueModel
                                        {
                                            Id = h.Id,
                                            Name = h.Name,
                                            Remark = h.Remark,
                                        },
                                    }
                                })).ToList();
            return profile;
        }
    }
}
