﻿using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Collections.Generic;

namespace irespository.purchase
{
    public interface IPurchaseGoodsBillnoRespository
    {
        PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByHospital(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalDepartmentId);
        PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId);
        PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated);
        int UpdateStatus(int id, BillStatus status);
        PurchaseGoodsBillnoListApiModel GetIndex(int id);
        IList<PurchaseGoodsBillnoListApiModel> GetListByPurchaseGoodsId(int purchaseGoodsId);
    }
}
