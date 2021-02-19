﻿using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;

namespace domain.purchase
{
    public class PurchaseContext
    {
        private readonly IPurchaseRespository _purchaseRespository;
        public PurchaseContext(IPurchaseRespository purchaseRespository)
        {
            _purchaseRespository = purchaseRespository;
        }

        public PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query)
        {
            return _purchaseRespository.GetPagerList(query);
        }
        public Purchase Create(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            return _purchaseRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _purchaseRespository.Delete(id);
        }
        public int Update(int id, PurchaseUpdateApiModel updated)
        {
            return _purchaseRespository.Update(id, updated);
        }
        public PurchaseIndexApiModel GetIndex(int id)
        {
            var goods = _purchaseRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _purchaseRespository.UpdateStatus(id, PurchaseStatus.Submited);
        }
        public int Comfirm(int id)
        {
            return _purchaseRespository.UpdateStatus(id, PurchaseStatus.Comfirmed);
        }
    }
}