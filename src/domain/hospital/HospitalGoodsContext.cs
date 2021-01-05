using foundation.config;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.goods.model;
using irespository.hospital.model;

namespace domain.hospital
{
    public class HospitalGoodsContext
    {
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;
        public HospitalGoodsContext(IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }

        public PagerResult<HospitalGoodsListApiModel> GetPagerList(PagerQuery<HospitalGoodsListQueryModel> query)
        {
            return _hospitalGoodsRespository.GetPagerList(query);
        }
        public HospitalGoods Create(HospitalGoodsCreateApiModel created, int userId)
        {
            return _hospitalGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _hospitalGoodsRespository.Delete(id);
        }
        public int Update(HospitalGoodsUpdateApiModel updated)
        {
            return _hospitalGoodsRespository.Update(updated);
        }
    }
}
