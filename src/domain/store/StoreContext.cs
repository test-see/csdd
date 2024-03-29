﻿using foundation.config;
using foundation.ef5.poco;
using foundation.exception;
using irespository.data;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.store
{
    public class StoreContext
    {
        private readonly object balance = new object();
        private readonly IStoreRespository _storeRespository;
        private readonly IStoreChangeTypeRespository _storeChangeTypeRespository;
        public StoreContext(IStoreRespository storeRespository,
            IStoreChangeTypeRespository storeChangeTypeRespository)
        {
            _storeRespository = storeRespository;
            _storeChangeTypeRespository = storeChangeTypeRespository;
        }

        public async Task<PagerResult<StoreListApiModel>> GetPagerListAsync(PagerQuery<StoreListQueryModel> query)
        {
            return await _storeRespository.GetPagerListAsync(query);
        }
        public async Task<IList<StoreListApiModel>> GetListByDepartmentAsync(int departmentId)
        {
            return await _storeRespository.GetListByDepartmentAsync(departmentId);
        }

        public bool BatchCreateOrUpdate(BatchStoreChangeApiModel created, int department, int userId)
        {
            lock (balance)
            {
                var changetype = _storeChangeTypeRespository.GetIndex(created.ChangeTypeId);
                foreach (var item in created.HospitalChangeGoods)
                {
                    var store = _storeRespository.GetIndexByGoods(department, item.HospitalGoodId);
                    var afterqty = (store?.Qty ?? 0) + changetype.Operator * item.ChangeQty;
                    if (afterqty < 0)
                        throw new DefaultException("库存不足!");
                    _storeRespository.CreateOrUpdate(item, afterqty, created.ChangeTypeId, department, userId);
                }
            }
            return true;
        }

        public int CreateOrUpdate(StoreChangeApiModel created, int department, int userId)
        {
            var changetype = _storeChangeTypeRespository.GetIndex(created.ChangeTypeId);
            lock (balance)
            {
                var store = _storeRespository.GetIndexByGoods(department, created.HospitalChangeGoods.HospitalGoodId);
                var afterqty = (store?.Qty ?? 0) + changetype.Operator * created.HospitalChangeGoods.ChangeQty;
                if (afterqty < 0)
                    throw new DefaultException("库存不足!");
                return _storeRespository.CreateOrUpdate(created.HospitalChangeGoods, afterqty, created.ChangeTypeId, department, userId);

            }
        }

        public Store GetIndexByGoods(int department, int goods)
        {
            return _storeRespository.GetIndexByGoods(department, goods);
        }
    }
}
