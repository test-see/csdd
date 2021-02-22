using foundation.ef5.poco;
using irespository.hospital.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iservice.hospital
{
    public interface IHospitalGoodsClientService
    {
        IList<HospitalGoodsClientListApiModel> GeListByGoodsId(int goodsId);
        HospitalGoodsClient Create(int goodsId, int clientId, int userId);
        int Delete(int id);
    }
}
