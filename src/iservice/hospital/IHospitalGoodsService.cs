using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.goods.model;
using irespository.hospital.model;

namespace iservice.hospital
{
    public interface IHospitalGoodsService
    {
        PagerResult<HospitalGoodsListApiModel> GetPagerList(PagerQuery<HospitalGoodsListQueryModel> query);
        HospitalGoods Create(HospitalGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalGoodsUpdateApiModel updated, int userId);
        HospitalGoodsIndexApiModel GetIndex(int id);
        HospitalGoods UpdateIsActive(int id, bool isActive);
        HospitalGoodsValueModel GetValueByBarcode(string barcode);
    }
}
