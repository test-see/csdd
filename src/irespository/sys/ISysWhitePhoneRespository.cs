using foundation.ef5.poco;

namespace irespository.user
{
    public interface ISysWhitePhoneRespository
    {
        bool Exists(string phone);
    }
}
