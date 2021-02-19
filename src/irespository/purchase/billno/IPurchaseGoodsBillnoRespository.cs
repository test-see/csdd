using foundation.config;
using foundation.ef5.poco;
using irespository.purchase.model;

namespace irespository.purchase
{
    public interface IPurchaseGoodsBillnoRespository
    {
        PagerResult<PurchaseGoodsBillnoListApiModel> GetPagerList(PagerQuery<PurchaseGoodsBillnoListQueryModel> query);
        PurchaseGoodsBillno Create(PurchaseGoodsBillnoCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, PurchaseGoodsBillnoUpdateApiModel updated);
    }
}
