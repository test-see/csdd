using domain.eventlog.valueobjects;
using irespository.eventlog;
using irespository.eventlog.model;
using irespository.hospital;
using irespository.sys.model;
using System.Collections.Generic;

namespace domain.eventlog
{
    public class EventlogHospitalGoodsContext
    {
        private readonly IEventlogHospitalGoodsRespository _eventlogHospitalGoodsRespository;
        private readonly IHospitalGoodsRespository _hospitalGoodsRespository;

        public EventlogHospitalGoodsContext(IEventlogHospitalGoodsRespository eventlogHospitalGoodsRespository,
            IHospitalGoodsRespository hospitalGoodsRespository)
        {
            _eventlogHospitalGoodsRespository = eventlogHospitalGoodsRespository;
            _hospitalGoodsRespository = hospitalGoodsRespository;
        }
        public void Create(EventlogHospitalGoodsChangeValueModel changed, int userId)
        {
            var goods = _hospitalGoodsRespository.Get(changed.GoodId);
            var log = new EventlogHospitalGoodsCreateApiModel
            {
                GoodsId = changed.GoodId,
                Title = $"修改药品 {goods.Name}",
                Content = changed.ChangeValue == null ?
                    "没有记录细节" :
                    $"修改药品信息 {changed.ChangeValue.Field} 从 {changed.ChangeValue.Source} 改为 {changed.ChangeValue.Result}",
            };
            _eventlogHospitalGoodsRespository.Create(log, userId);
        }
        public IList<EventlogListApiModel> GetList(int goodsId)
        {
            return _eventlogHospitalGoodsRespository.GetList(goodsId);
        }
    }

}