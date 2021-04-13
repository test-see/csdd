using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using System.Collections.Generic;
using System.Linq;

namespace irespository.hospital
{
    public interface IHospitalGoodsRespository
    {
        PagerResult<HospitalGoodsStoreListApiModel> GetPagerStoreList(PagerQuery<HospitalGoodsListQueryModel> query, int departmentId);
        HospitalGoods Create(HospitalGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalGoodsUpdateApiModel updated);
        HospitalGoods Get(int id);
        HospitalGoods UpdateIsActive(int id, bool isActive);
        HospitalGoodsValueModel GetValueByBarcode(string barcode);
    }
}
