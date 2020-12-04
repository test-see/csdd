using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.client
{
    public interface IClientRespository
    {
        IEnumerable<Client> GetListByProvince(int provinceId);
    }
}
