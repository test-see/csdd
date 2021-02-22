using foundation.config;
using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.hospital
{
    public interface IHospitalGoodsClientRespository
    {
        IList<IdNameValueModel> GeClientList(int goodsId);
        HospitalGoodsClient Create(int goodsId, int clientId, int userId);
        int Delete(int id);
    }
}
