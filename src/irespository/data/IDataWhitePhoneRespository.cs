using foundation.ef5.poco;

namespace irespository.user
{
    public interface IDataWhitePhoneRespository
    {
        DataWhitePhone GetByPhone(string phone);
    }
}
