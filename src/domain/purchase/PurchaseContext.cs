using foundation.config;
using foundation.ef5.poco;
using irespository.purchase;
using irespository.purchase.model;
using irespository.purchase.profile.enums;
using System.Threading.Tasks;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Services.Interfaces;

namespace domain.purchase
{
    public class PurchaseContext
    {
        private readonly IPurchaseRespository _purchaseRespository;
        private readonly PurchaseSettingThresholdContext _purchaseSettingThresholdContext;
        private readonly PurchaseGoodsContext _purchaseGoodsContext;
        private readonly IProducingService _bus;
        public PurchaseContext(IPurchaseRespository purchaseRespository,
            PurchaseSettingThresholdContext purchaseSettingThresholdContext,
            PurchaseGoodsContext purchaseGoodsContext,
            IProducingService bus)
        {
            _purchaseRespository = purchaseRespository;
            _purchaseSettingThresholdContext = purchaseSettingThresholdContext;
            _purchaseGoodsContext = purchaseGoodsContext;
            _bus = bus;
        }

        public async Task<PagerResult<PurchaseListApiModel>> GetPagerListAsync(PagerQuery<PurchaseListQueryModel> query)
        {
            return await _purchaseRespository.GetPagerListAsync(query);
        }
        public async Task<Purchase> CreateAsync(PurchaseCreateApiModel created, int departmentId, int userId)
        {
            var purchase = _purchaseRespository.Create(created, departmentId, userId);
            if (created.PurchaseSettingId != null)
            {
                await _bus.SendAsync(new RabbitMqMessage<int> { Payload = purchase.Id }, "exchange.name", "purchase.generate");
                _purchaseRespository.UpdateStatus(purchase.Id, PurchaseStatus.Generating);
            }
            return purchase;
        }
        public async Task GenerateAsync(int id)
        {
            var purchase = await GetIndexAsync(id);
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
        public async Task<PurchaseIndexApiModel> GetIndexAsync(int id)
        {
            var goods = await _purchaseRespository.GetIndexAsync(id);
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
