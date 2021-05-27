using storage.adapter.v2.client;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.v2.client
{
    public class ClientGoodsService
    {
        private readonly IClientGoodsRespository _clientGoodsRespository;
        public ClientGoodsService(IClientGoodsRespository clientGoodsRespository)
        {
            _clientGoodsRespository = clientGoodsRespository;
        }
    }
}
