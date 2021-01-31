﻿using foundation.config;
using foundation.ef5.poco;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;

namespace iservice.store
{
    public interface IStoreService
    {
        PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query);
        Store CustomizeCreate(CustomizeStoreChangeApiModel created, int department, int userId);
        IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList();
        Store GetIndexByGoods(int department, int goods);
    }
}
