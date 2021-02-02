using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;

namespace irespository.prescription
{
    public interface IPrescriptionRespository
    {
        PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query);
        Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId);
    }
}
