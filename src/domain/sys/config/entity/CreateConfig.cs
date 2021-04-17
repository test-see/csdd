using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class CreateConfig : IRequest
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
    }
}
