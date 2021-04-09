using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalGoodsClientRespository
    {
        IList<HospitalGoodsClientListApiModel> GetListByGoodsId(int goodsId);
        PagerResult<HospitalGoodsClientListApiModel> GetPagerList(PagerQuery<HospitalGoodsClientQueryModel> query);
        HospitalGoodsClient Create(int goodsId, int clientId, int userId);
        int Delete(int id);
    }
}
