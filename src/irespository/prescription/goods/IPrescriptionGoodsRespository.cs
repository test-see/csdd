using foundation.ef5.poco;
using irespository.prescription.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.prescription
{
    public interface IPrescriptionGoodsRespository
    {
        Task<IList<PrescriptionGoodsListApiModel>> GetListAsync(int prescriptionId);
    }
}
