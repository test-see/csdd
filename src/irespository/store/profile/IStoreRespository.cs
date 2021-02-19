﻿using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;

namespace irespository.store
{
    public interface IStoreRespository
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        int BatchCreateOrUpdate(BatchStoreChangeApiModel created, int departmentId, int userId);
        int CreateOrUpdate(StoreChangeApiModel created, int departmentId, int userId);
        Store GetIndexByGoods(int department, int goods);
    
    }
}
