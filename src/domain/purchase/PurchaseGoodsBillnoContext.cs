﻿using domain.store;
using domain.store.enums;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;

namespace domain.purchase
{
    public class PurchaseGoodsBillnoContext
    {
        private readonly IPurchaseGoodsBillnoRespository _PurchaseGoodsBillnoRespository;
        private readonly StoreContext _storeContext;
        public PurchaseGoodsBillnoContext(IPurchaseGoodsBillnoRespository purchaseGoodsBillnoRespositoryy,
            StoreContext storeContext)
        {
            _PurchaseGoodsBillnoRespository = purchaseGoodsBillnoRespositoryy;
            _storeContext = storeContext;
        }

        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query)
        {
            return _PurchaseGoodsBillnoRespository.GetPagerList(query);
        }


        public PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId)
        {
            return _PurchaseGoodsBillnoRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _PurchaseGoodsBillnoRespository.Delete(id);
        }
        public int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            return _PurchaseGoodsBillnoRespository.Update(id, updated);
        }

        public int Comfirm(IList<int> ids, int userId)
        {
            foreach (var id in ids)
            {
                var goods = _PurchaseGoodsBillnoRespository.GetIndex(id);
                _storeContext.CreateOrUpdate(new StoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.Purchase,
                    HospitalChangeGoods = new StoreChangeGoodsValueModel
                    {
                        HospitalGoodId = goods.HospitalGoods.Id,
                        Qty = goods.Qty,
                    },
                }, goods.Purchase.HospitalDepartment.Id, userId);
                _PurchaseGoodsBillnoRespository.UpdateStatus(id, BillStatus.Comfirmed);
            }
            return ids.Count;
        }
    }
}
