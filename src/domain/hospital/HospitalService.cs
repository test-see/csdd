using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalService
    {
        private readonly IMediator _mediator;
        public HospitalService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Hospital> UpdateAsync(UpdateHospitalRequest updated)
        {
            return await _mediator.RequestSingleAsync<UpdateHospitalRequest, Hospital>(updated);
        }

        public async Task<Hospital> CreateAsync(CreateHospitalRequest created)
        {
            return await _mediator.RequestSingleAsync<CreateHospitalRequest, Hospital>(created);
        }
        public async Task DeleteAsync(DeleteHospitalCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
