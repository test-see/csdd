using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital;
using irespository.hospital.department.model;
using irespository.hospital.goods.model;
using irespository.hospital.profile.model;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;
using irespository.store.record.model;
using Mediator.Net;
using storage.hospital.department.carrier;
using storage.hospitalgoods.carrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace respository.store
{
    public class StoreRespository : IStoreRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IStoreRecordRespository _storeRecordRespository;
        private readonly IMediator _mediator;
        public StoreRespository(DefaultDbContext context,
            IMediator mediator,
            IStoreRecordRespository storeRecordRespository)
        {
            _context = context;
            _mediator = mediator;
            _storeRecordRespository = storeRecordRespository;
        }
        public int CreateOrUpdate(StoreChangeGoodsValueModel created, int afterQty, int changeTypeId, int departmentId, int userId)
        {
            var beforeStore = GetIndexByGoods(departmentId, created.HospitalGoodId);
            var record = _storeRecordRespository.CreateAsync(new StoreRecordCreateApiModel
            {
                BeforeQty = beforeStore?.Qty ?? 0,
                ChangeQty = created.ChangeQty,
                ChangeTypeId = changeTypeId,
                HospitalDepartmentId = departmentId,
                HospitalGoodsId = created.HospitalGoodId,
                Recrdno = created.Recrdno,
            }, userId);

            if (beforeStore == null) Create(created.HospitalGoodId, afterQty, departmentId, userId);
            else Update(beforeStore, afterQty, userId);

            return record.Id;
        }

        private void Create(int hospitalGoodId, int afterQty, int department, int userId)
        {
            var store = new Store
            {
                CreateTime = DateTime.Now,
                CreateUserId = userId,
                HospitalDepartmentId = department,
                HospitalGoodsId = hospitalGoodId,
                Qty = afterQty,
                UpdateTime = DateTime.Now,
                UpdateUserId = userId,
            };
            _context.Store.Add(store);
            _context.SaveChanges();
        }

        private void Update(Store store, int afterQty, int userId)
        {
            store.Qty = afterQty;
            store.UpdateTime = DateTime.Now;
            store.UpdateUserId = userId;
            _context.Store.Update(store);
            _context.SaveChanges();
        }

        public Store GetIndexByGoods(int department, int goods)
        {
            return _context.Store.FirstOrDefault(x => x.HospitalDepartmentId == department && x.HospitalGoodsId == goods);
        }

        public async Task<PagerResult<StoreListApiModel>> GetPagerListAsync(PagerQuery<StoreListQueryModel> query)
        {
            var sql = from r in _context.Store
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join uu in _context.User on r.UpdateUserId equals uu.Id
                      select new StoreListApiModel
                      {
                          Id = r.Id,
                          CreateTime = r.CreateTime,
                          CreateUserName = uc.Username,
                          UpdateTime = r.UpdateTime,
                          UpdateUserName = uu.Username,
                          Qty = r.Qty,
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
            var data = new PagerResult<StoreListApiModel>(query.Index, query.Size, sql);
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
        public async Task<IList<StoreListApiModel>> GetListByDepartmentAsync(int departmentId)
        {
            var sql = from r in _context.Store
                      join uc in _context.User on r.CreateUserId equals uc.Id
                      join uu in _context.User on r.UpdateUserId equals uu.Id
                      where r.HospitalDepartmentId == departmentId
                      select new StoreListApiModel
                      {
                          Id = r.Id,
                          CreateTime = r.CreateTime,
                          CreateUserName = uc.Username,
                          UpdateTime = r.UpdateTime,
                          UpdateUserName = uu.Username,
                          Qty = r.Qty,
                          HospitalDepartment = new GetHospitalDepartmentResponse
                          {
                              Id = r.HospitalDepartmentId,
                          },
                          HospitalGoods = new GetHospitalGoodsResponse
                          {
                              Id = r.HospitalGoodsId,
                          },
                      };
            var data = sql.ToList();
            var departments = await _mediator.RequestListByIdsAsync<GetHospitalDepartmentRequest, GetHospitalDepartmentResponse>(data.Select(x => x.HospitalDepartment.Id).ToList());
            var goods = await _mediator.RequestListByIdsAsync<GetHospitalGoodsRequest, GetHospitalGoodsResponse>(data.Select(x => x.HospitalGoods.Id).ToList());
            foreach (var m in data)
            {
                m.HospitalGoods = goods.FirstOrDefault(x => x.Id == m.HospitalGoods.Id);
                m.HospitalDepartment = departments.FirstOrDefault(x => x.Id == m.HospitalDepartment.Id);
            }
            return data;
        }
    }
}
