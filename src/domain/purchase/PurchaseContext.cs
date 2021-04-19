using EasyNetQ;
using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Threading.Tasks;

namespace domain.purchase
{
    public class PurchaseContext
    {
        private readonly IPurchaseRespository _purchaseRespository;
        private readonly PurchaseSettingThresholdContext _purchaseSettingThresholdContext;
        private readonly PurchaseGoodsContext _purchaseGoodsContext;
        private readonly IBus _bus;
        public PurchaseContext(IPurchaseRespository purchaseRespository,
            PurchaseSettingThresholdContext purchaseSettingThresholdContext,
            PurchaseGoodsContext purchaseGoodsContext,
            IBus bus)
        {
            _purchaseRespository = purchaseRespository;
            _purchaseSettingThresholdContext = purchaseSettingThresholdContext;
            _purchaseGoodsContext = purchaseGoodsContext;
            _bus = bus;
        }

        public PagerResult<PurchaseListApiModel> GetPagerList(PagerQuery<PurchaseListQueryModel> query)
        {
            return _purchaseRespository.GetPagerList(query);
        }
        public async Task<Purchase> CreateAsync(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            var purchase = _purchaseRespository.Create(created, departmentId, userId);
            if (created.PurchaseSettingId != null)
            {
                await _bus.PubSub.PublishAsync(new RabbitMqMessage<int> { Payload = purchase.Id }, "Purchase.Generate");
                _purchaseRespository.UpdateStatus(purchase.Id, PurchaseStatus.Generating);
            }
            return purchase;
        }
        public async Task GenerateAsync(int id)
        {
            var purchase = GetIndex(id);
            if (purchase.PurchaseSettingId != null)
            {
                var thresholds = _purchaseSettingThresholdContext.GetListBySettingId(purchase.PurchaseSettingId.Value);
                foreach (var item in thresholds)
                {
                    await _purchaseGoodsContext.GenerateAsync(purchase.Id, item, purchase.HospitalDepartment.Id, purchase.CreateUserId);
                }
            }
            _purchaseRespository.UpdateStatus(purchase.Id, PurchaseStatus.Pendding);
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
        public int Revoke(int id)
        {
            return _purchaseRespository.UpdateStatus(id, PurchaseStatus.Pendding);
        }
    }
}
