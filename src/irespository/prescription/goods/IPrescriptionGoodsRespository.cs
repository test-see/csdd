using foundation.ef5.poco;
using irespository.prescription.model;
using System.Collections.Generic;

namespace irespository.prescription
{
    public interface IPrescriptionGoodsRespository
    {
        IList<PrescriptionGoodsListApiModel> GetList(int prescriptionId);
    }
}
