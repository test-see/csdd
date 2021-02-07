using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.checklist;
using irespository.checklist.model;
using System;
using System.Linq;

namespace respository.checklist
{
    public class CheckListGoodsRespository : ICheckListGoodsRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public CheckListGoodsRespository(DefaultDbContext context,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _context = context;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }
        public PagerResult<CheckListGoodsApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query)
        {
            var sql = from r in _context.CheckListGoods
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new CheckListGoodsApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          CheckQty = r.CheckQty,
                          HospitalGoods = new HospitalGoodsValueModel
                          {
                              Id = r.HospitalGoodsId,
                          },
                          CreateUsername = u.Username,
                          StoreQty = r.StoreQty,
                      };
            var data = new PagerResult<CheckListGoodsApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = _hospitalGoodsRespository.GetValue(m.HospitalGoods.Id);
                }
            }
            return data;
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
                StoreQty = 0,
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
