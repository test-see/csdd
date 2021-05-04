using Mediator.Net.Contracts;

namespace irespository.sys.model
{
    public class ListWhitePhoneRequest:IRequest
    {
        public string Phone { get; set; }
    }
}
