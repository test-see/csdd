using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.goods.model;
using irespository.client.model;
using irespository.client.profile.model;
using irespository.hospital;
using irespository.hospital.goods.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientGoodsRespository : IClientGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public ClientGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
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
                          Code = r.Code,
                          Client = new ClientValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,
                      };
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            if (!string.IsNullOrEmpty(query.Query?.Code))
            {
                sql = sql.Where(x => x.Code.Contains(query.Query.Code));
            }
            if (query.Query?.IsActive != null)
            {
                sql = sql.Where(x => x.IsActive == query.Query.IsActive);
            }
            return new PagerResult<ClientGoodsListApiModel>(query.Index, query.Size, sql);
        }

        public ClientGoods Create(ClientGoodsCreateApiModel created, int userId)
        {
            var goods = new ClientGoods
            {
                Code = created.Code,
                Name = created.Name,
                ClientId = created.ClientId,
                Producer = created.Producer,
                Spec = created.Spec,
                Unit = created.Unit,
                CreateUserId = userId,
                IsActive = 1,
                CreateTime = DateTime.Now,
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

            goods.Code = updated.Code;
            goods.Name = updated.Name;
            goods.Producer = updated.Producer;
            goods.Spec = updated.Spec;
            goods.Unit = updated.Unit;
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
                          Code = r.Code,
                          Client = new ClientValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,
                          CreateUserName = u.Username,
                          IsActive = r.IsActive,

                      };

            var profile = sql.FirstOrDefault();
            if (profile != null)
            {
                var mappings = (from m in _context.ClientMappingGoods
                                select new ClientMappingGoodsListApiModel
                                {
                                    Id = m.Id,
                                    ClientQty = m.ClientQty,
                                    HospitalQty = m.HospitalQty,
                                    HospitalGoods = new HospitalGoodsValueModel
                                    {
                                        Id = m.HospitalGoodsId,
                                    }
                                }).ToList();
                var goods = _hospitalGoodsRespository.GetValue(mappings.Select(x => x.HospitalGoods.Id).ToArray());
                foreach (var m in mappings)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
                profile.Mappings = mappings;
            }
            return profile;
        }

        public IList<ClientGoodsValueModel> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<ClientGoodsValueModel>();
            var sql = from r in _context.ClientGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      join h in _context.Client on r.ClientId equals h.Id
                      where ids.Contains(r.Id)
                      select new ClientGoodsValueModel
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Code = r.Code,
                          Client = new ClientValueModel
                          {
                              Id = h.Id,
                              Name = h.Name,
                          },
                          Producer = r.Producer,
                          Spec = r.Spec,
                          Unit = r.Unit,

                      };

            var profile = sql.ToList();
            return profile;
        }

    }
}
