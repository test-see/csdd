﻿using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using System.Collections.Generic;

namespace iservice.purchase
{
    public interface IPurchaseGoodsBillnoService
    {
        PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query);
        PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated);
        int Comfirm(IList<int> ids);
    }
}