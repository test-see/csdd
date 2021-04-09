using irespository.hospital.profile.model;
using Mediator.Net.Contracts;
using nouns.client.profile;

namespace irespository.hospital.client.model
{
    public class GetHospitalClientResponse : IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetHospitalResponse Hospital { get; set; }
        public GetClientResponse Client { get; set; }
    }
}
