using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Collections.Generic;

namespace service.purchase
{
    public class PurchaseGoodsBillnoService : IPurchaseGoodsBillnoService
    {
        private readonly PurchaseGoodsBillnoContext _PurchaseGoodsBillnoContext;
        public PurchaseGoodsBillnoService(PurchaseGoodsBillnoContext PurchaseGoodsBillnoContext)
        {
            _PurchaseGoodsBillnoContext = PurchaseGoodsBillnoContext;
        }
        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByHospital(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalId)
        {
            return _PurchaseGoodsBillnoContext.GetPagerListByHospital(query, hospitalId);
        }
        public PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerListByClient(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId)
        {
            return _PurchaseGoodsBillnoContext.GetPagerListByClient(query, clientId);
        }

        public PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId)
        {
            return _PurchaseGoodsBillnoContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _PurchaseGoodsBillnoContext.Delete(id);
        }

        public int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated)
        {
            return _PurchaseGoodsBillnoContext.Update(id, updated);
        }

        public int Comfirm(IList<int> ids, int userId)
        {
            return _PurchaseGoodsBillnoContext.Comfirm(ids, userId);
        }
    }
}
