using foundation.ef5.poco;

namespace irespository.user
{
    public interface IDataWhitePhoneRespository
    {
        bool Exists(string phone);
    }
}
