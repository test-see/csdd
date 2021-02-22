using foundation.ef5.poco;
using irespository.hospital.model;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalGoodsClientRespository
    {
        IList<HospitalGoodsClientListApiModel> GeListByGoodsId(int goodsId);
        HospitalGoodsClient Create(int goodsId, int clientId, int userId);
        int Delete(int id);
    }
}
