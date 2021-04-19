using domain.client.profile.entity;
using foundation.ef5.poco;
using foundation.mediator;
using irespository.hospital.model;
using Mediator.Net;
using System.Threading.Tasks;

namespace domain.hospital
{
    public class HospitalClientService
    {
        private readonly IMediator _mediator;
        public HospitalClientService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<HospitalClient> UpdateAsync(UpdateHospitalClientRequest updated)
        {
            return await _mediator.RequestAsync<UpdateHospitalClientRequest, HospitalClient>(updated);
        }

        public async Task<HospitalClient> CreateAsync(CreateHospitalClientRequest created)
        {
            return await _mediator.RequestAsync<CreateHospitalClientRequest, HospitalClient>(created);
        }
        public async Task DeleteAsync(DeleteHospitalClientCommand deleted)
        {
            await _mediator.SendAsync(deleted);
        }
    }
}
