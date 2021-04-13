using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using nouns.client.profile;
using System.Collections.Generic;

namespace irespository.client
{
    public interface IClientRespository
    {
        GetClientResponse GetIndex(int id);
        IList<GetClientResponse> GetValue(int[] ids);
    }
}
