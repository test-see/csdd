using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.purchase
{
    public class PurchaseGoodsBillnoService : IPurchaseGoodsBillnoService
    {
        private readonly PurchaseGoodsBillnoContext _PurchaseGoodsBillnoContext;
        public PurchaseGoodsBillnoService(PurchaseGoodsBillnoContext PurchaseGoodsBillnoContext)
        {
            _PurchaseGoodsBillnoContext = PurchaseGoodsBillnoContext;
        }
        public async Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByHospitalAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int hospitalId)
        {
            return await _PurchaseGoodsBillnoContext.GetPagerListByHospitalAsync(query, hospitalId);
        }
        public async Task<PagerResult<PurchaseGoodsBillnoListApiModel>> GetPagerListByClientAsync(PagerQuery<PurchaseGoodsBillnoListQueryModel> query, int clientId)
        {
            return await _PurchaseGoodsBillnoContext.GetPagerListByClientAsync(query, clientId);
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

        public async Task<int> ComfirmAsync(IList<int> ids, int userId)
        {
            return await _PurchaseGoodsBillnoContext.ComfirmAsync(ids, userId);
        }
    }
}
