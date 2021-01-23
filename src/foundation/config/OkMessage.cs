using System;
using System.Net;

namespace foundation.config
{
    [Serializable]
    public class OkMessage<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
        public OkMessage() { }
        public OkMessage(T data) { Data = data; Code = (int)HttpStatusCode.OK; }
        public OkMessage(int code, string msg) { Code = code; Msg = msg; }
    }
}
