using domain.hospital;
using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using iservice.hospital;

namespace service.hospital
{
    public class HospitalGoodsService : IHospitalGoodsService
    {
        private readonly HospitalGoodsContext _hospitalGoodsContext;
        public HospitalGoodsService(HospitalGoodsContext hospitalGoodsContext)
        {
            _hospitalGoodsContext = hospitalGoodsContext;
        }
        public PagerResult<HospitalGoodsListApiModel> GetPagerList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            return _hospitalGoodsContext.GetPagerList(query);
        }
        public HospitalGoods Create(HospitalGoodsCreateApiModel created, int userId)
        {
            return _hospitalGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _hospitalGoodsContext.Delete(id);
        }

        public int Update(int id, HospitalGoodsUpdateApiModel updated, int userId)
        {
            return _hospitalGoodsContext.Update(id, updated, userId);
        }

        public HospitalGoodsIndexApiModel GetIndex(int id)
        {
            return _hospitalGoodsContext.GetIndex(id);
        }

        public HospitalGoods UpdateIsActive(int id, bool isActive)
        {
            return _hospitalGoodsContext.UpdateIsActive(id, isActive);
        }

        public HospitalGoodsValueModel GetValueByBarcode(string barcode)
        {
            return _hospitalGoodsContext.GetValueByBarcode(barcode);
        }
    }
}
