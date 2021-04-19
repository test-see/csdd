using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;
using irespository.prescription.profile.enums;
using System.Threading.Tasks;

namespace irespository.prescription
{
    public interface IPrescriptionRespository
    {
        Task<PagerResult<PrescriptionListApiModel>> GetPagerListAsync(PagerQuery<PrescriptionListQueryModel> query, int hospitalId);
        Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId);
        int UpdateStatus(int id, PrescriptionStatus status);
        Task<PrescriptionIndexApiModel> GetIndexAsync(int id);
    }
}
