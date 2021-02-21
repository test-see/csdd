using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.prescription
{
    public interface IPrescriptionGoodsRespository
    {
        IList<PrescriptionGoods> GetList(int prescriptionId);
    }
}
