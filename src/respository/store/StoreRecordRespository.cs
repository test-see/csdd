using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.store;
using irespository.store.profile.model;
using irespository.store.record.model;
using Mediator.Net;
using storage.hospital.department.carrier;
using storage.hospitalgoods.carrier;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace respository.store
{
    public class StoreRecordRespository : IStoreRecordRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IMediator _mediator;
        public StoreRecordRespository(DefaultDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<PagerResult<StoreRecordListApiModel>> GetPagerListAsync(PagerQuery<StoreRecordListQueryModel> query)
        {
            var sql = from r in _context.StoreRecord
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      orderby r.Id descending
                      select new StoreRecordListApiModel
                      {
                          Id = r.Id,
                          CreateTime = r.CreateTime,
                          CreateUserName = uc.Username,
                          ChangeQty = r.ChangeQty,
                          BeforeQty = r.BeforeQty,
                          Price = r.Price,
                          ChangeType = ct,
                          HospitalDepartment = new GetHospitalDepartmentResponse
                          {
                              Id = r.HospitalDepartmentId,
                          },
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            if (query.Query?.HospitalDepartmentId != null)
            {
                sql = sql.Where(x => x.HospitalDepartment.Id == query.Query.HospitalDepartmentId.Value);
            }
            if (query.Query?.HospitalGoodsId != null)
            {
                sql = sql.Where(x => x.HospitalGoods.Id == query.Query.HospitalGoodsId.Value);
            }
            if (query.Query?.BeginDate != null)
            {
                sql = sql.Where(x => x.CreateTime >= query.Query.BeginDate.Value);
            }
            if (query.Query?.EndDate != null)
            {
                sql = sql.Where(x => x.CreateTime < query.Query.EndDate.Value.AddDays(1));
            }
            var data = new PagerResult<StoreRecordListApiModel>(query.Index, query.Size, sql);
            if (data.Total > 0)
            {
                var departments = await _mediator.RequestListByIdsAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(data.Select(x => x.HospitalDepartment.Id));
                var goods = await _mediator.RequestListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
                foreach (var m in data.Result)
                {
                    m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                    m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
                }
            }
            return data;
        }

        public async Task<StoreRecord> CreateAsync(StoreRecordCreateApiModel created, int userId)
        {
            var goods = await _mediator.RequestSingleByIdAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(created.HospitalGoodsId );
            var record = new StoreRecord
            {
                BeforeQty = created.BeforeQty,
                ChangeTypeId = created.ChangeTypeId,
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = created.HospitalDepartmentId,
                HospitalGoodsId = created.HospitalGoodsId,
                Price = goods.Price,
                ChangeQty = created.ChangeQty,
                Recrdno = created.Recrdno,
            };

            _context.StoreRecord.Add(record);
            _context.SaveChanges();

            return record;
        }

        public int GetConsumeAmount(int deparmentId, int goodsId, int days)
        {
            var limitdate = DateTime.Today.AddDays(-days);
            var sql = from r in _context.StoreRecord
                      join ct in _context.DataStoreChangeType on r.ChangeTypeId equals ct.Id
                      where r.HospitalDepartmentId == deparmentId && r.HospitalGoodsId == goodsId
                      && ct.IsConsume == 1
                      && r.CreateTime >= limitdate
                      select r.ChangeQty;

            return sql.Sum();
        }
    }
}
