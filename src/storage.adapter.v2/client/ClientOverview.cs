using foundation.ef5.poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace storage.adapter.v2.client
{
    public class ClientOverview
    {
        public Client Client { get; set; }
        public User User { get; set; }
    }
}
