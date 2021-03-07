using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;

namespace iservice.prescription
{
    public interface IPrescriptionService
    {
        PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query, int hospitalId);
        Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId);
        int Submit(int id, int userId);
    }
}
