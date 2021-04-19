using domain.purchase;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;
using iservice.purchase;
using System.Threading.Tasks;

namespace service.purchase
{
    public class PurchaseService : IPurchaseService
    {
        private readonly PurchaseContext _purchaseContext;
        public PurchaseService(PurchaseContext PurchaseContext)
        {
            _purchaseContext = PurchaseContext;
        }
        public PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query)
        {
            return _purchaseContext.GetPagerList(query);
        }
        public async Task<Purchase> CreateAsync(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            return await _purchaseContext.CreateAsync(created, departmentId, userId);
        }
        public async Task GenerateAsync(int id)
        {
            await _purchaseContext.GenerateAsync(id);
        }

        public int Delete(int id)
        {
            return _purchaseContext.Delete(id);
        }

        public int Update(int id, PurchaseUpdateApiModel updated)
        {
            return _purchaseContext.Update(id, updated);
        }

        public PurchaseIndexApiModel GetIndex(int id)
        {
            return _purchaseContext.GetIndex(id);
        }

        public int Submit(int id)
        {
            return _purchaseContext.Submit(id);
        }
        public int Comfirm(int id)
        {
            return _purchaseContext.Comfirm(id);
        }
        public int Revoke(int id)
        {
            return _purchaseContext.Revoke(id);
        }
    }
}
