using foundation.ef5.poco;
using irespository.eventlog.model;
using irespository.sys.model;
using System.Collections.Generic;

namespace irespository.eventlog
{
    public interface IEventlogHospitalGoodsRespository
    {
        EventlogHospitalGoods Create(EventlogHospitalGoodsCreateApiModel created, int userId);
        IList<ListEventlogResponse> GetList(int goodsId);
    }
}
