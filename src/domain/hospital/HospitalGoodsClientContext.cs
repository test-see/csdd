using foundation.config;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using System.Collections.Generic;

namespace domain.hospital
{
    public class HospitalGoodsClientContext
    {
        private readonly IHospitalGoodsClientRespository _hospitalGoodsClientRespository;
        public HospitalGoodsClientContext(IHospitalGoodsClientRespository HospitalGoodsClientRespository)
        {
            _hospitalGoodsClientRespository = HospitalGoodsClientRespository;
        }

        public IList<HospitalGoodsClientListApiModel> GetListByGoodsId(int goodsId)
        {
            return _hospitalGoodsClientRespository.GetListByGoodsId(goodsId);
        }
        public PagerResult<HospitalGoodsClientListApiModel> GetPagerList(PagerQuery<HospitalGoodsClientQueryModel> query)
        {
            return _hospitalGoodsClientRespository.GetPagerList(query);
        }
        public HospitalGoodsClient Create(int goodsId, int clientId, int userId)
        {
            return _hospitalGoodsClientRespository.Create(goodsId, clientId, userId);
        }
        public int Delete(int id)
        {
            return _hospitalGoodsClientRespository.Delete(id);
        }
    }
}
