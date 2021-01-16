using domain.eventlog;
using domain.eventlog.valueobjects;
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
        private readonly EventlogHospitalGoodsContext _eventlogHospitalGoodsContext;
        public HospitalGoodsContext(IHospitalGoodsRespository hospitalGoodsRespository,
            EventlogHospitalGoodsContext eventlogHospitalGoodsContext)
        {
            _hospitalGoodsRespository = hospitalGoodsRespository;
            _eventlogHospitalGoodsContext = eventlogHospitalGoodsContext;
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
        public int Update(int id, HospitalGoodsUpdateApiModel updated, int userId)
        {
            _hospitalGoodsRespository.Update(id, updated);
            _eventlogHospitalGoodsContext.Create(new EventlogHospitalGoodsChangeValueModel
            {
                GoodId = id,
                ChangeValue = null,
            }, userId);
            return id;
        }
        public HospitalGoodsIndexApiModel GetIndex(int id)
        {
            var goods = _hospitalGoodsRespository.GetIndex(id);
            goods.Logs = _eventlogHospitalGoodsContext.GetList(id);
            return goods;
        }

        public HospitalGoods UpdateIsActive(int id, bool isActive)
        {
            return _hospitalGoodsRespository.UpdateIsActive(id, isActive);
        }
    }
}
