using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;
using System.Threading.Tasks;

namespace iservice.prescription
{
    public interface IPrescriptionService
    {
        PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query, int hospitalId);
        Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId);
        Task<int> SubmitAsync(int id, int userId);
        PrescriptionIndexApiModel GetIndex(int id);
    }
}
