using domain.prescription;
using foundation.config;
using foundation.ef5.poco;
using irespository.prescription.model;
using iservice.prescription;
using System.Threading.Tasks;

namespace service.prescription
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly PrescriptionContext _prescriptionContext;
        public PrescriptionService(PrescriptionContext prescriptionContext)
        {
            _prescriptionContext = prescriptionContext;
        }
        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            return _prescriptionContext.Create(created, departmentId, userId);
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query, int hospitalId)
        {
            return _prescriptionContext.GetPagerList(query, hospitalId);
        }
        public async Task<int> SubmitAsync(int id, int userId)
        {
            return await _prescriptionContext.SubmitAsync(id, userId);
        }
        public PrescriptionIndexApiModel GetIndex(int id)
        {
            return _prescriptionContext.GetIndex(id);
        }
    }
}
