using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.model;
using irespository.client.profile.model;
using System;
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
            var goods = _context.ClientGoods.Find(id);
            _context.ClientGoods.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, ClientGoodsUpdateApiModel updated)
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

            return sql.FirstOrDefault();
        }
    }
}
