using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.checklist;
using irespository.checklist.model;
using System;
using System.Linq;
using irespository.checklist.goods.model;
using System.Collections.Generic;
using Mediator.Net;
using foundation.mediator;
using storage.hospitalgoods.carrier;
using System.Threading.Tasks;

namespace respository.checklist
{
    public class CheckListGoodsRespository : ICheckListGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public CheckListGoodsRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<PagerResult<CheckListGoodsListApiModel>> GetPagerListAsync(PagerQuery<CheckListGoodsQueryModel> query)
        {
            var sql = from r in _context.CheckListGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new CheckListGoodsListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CheckQty = r.CheckQty,
                          CheckListId = r.CheckListId,
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                          CreateUsername = u.Username,
                          StoreQty = r.StoreQty,
                      };
            if (query.Query?.CheckListId != null)
            {
                sql = sql.Where(x => x.CheckListId == query.Query.CheckListId.Value);
            }
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => query.Query.HospitalGoodsId.Value == x.HospitalGoods.Id);
            }
            var data = new PagerResult<CheckListGoodsListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = await _mediator.RequestListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
            }
            return data;
        }

        public async Task<PagerResult<CheckListGoodsPreviewListApiModel>> GetPagerPreviewListAsync(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            var sql = from r in _context.CheckListGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.CheckListId == checkListId
                      select new CheckListGoodsPreviewListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CheckQty = r.CheckQty,
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                          CreateUsername = u.Username,
                          StoreQty = r.StoreQty,
                      };
            var data = new PagerResult<CheckListGoodsPreviewListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var goods = await _mediator.RequestListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                }
            }
            return data;
        }
        public async Task<IList<CheckListGoodsPreviewListApiModel>> GetPreviewListAsync(int checkListId)
        {
            var sql = from r in _context.CheckListGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      where r.CheckListId == checkListId && r.CheckQty != r.StoreQty
                      select new CheckListGoodsPreviewListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CheckQty = r.CheckQty,
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                          CreateUsername = u.Username,
                          StoreQty = r.StoreQty,
                      };
            var data = sql.ToList();
            var goods = await _mediator.RequestListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
            foreach (var m in data)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
            }
            return data;
        }

        public decimal GetPreviewListAmount(int checkListId)
        {
            var sql = from r in _context.CheckListGoods
                      join g in _context.HospitalGoods on r.HospitalGoodsId equals g.Id
                      where r.CheckListId == checkListId
                      select (r.CheckQty - r.StoreQty) * g.Price;
            return sql.Sum();
        }

        public CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId)
        {
            var setting = new CheckListGoods
            {
                CheckListId = created.CheckListId,
                HospitalGoodsId = created.HospitalGoodsId,
                CheckQty = created.CheckQty,
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                StoreQty = created.StoreQty,
            };

            _context.CheckListGoods.Add(setting);
            _context.SaveChanges();

            return setting;
        }

        public int Delete(int id)
        {
            var setting = _context.CheckListGoods.Find(id);
            _context.CheckListGoods.Remove(setting);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, CheckListGoodsUpdateApiModel updated)
        {
            var setting = _context.CheckListGoods.First(x => x.Id == id);
            setting.CheckQty = updated.CheckQty;

            _context.CheckListGoods.Update(setting);
            _context.SaveChanges();
            return setting.Id;
        }
    }
}
