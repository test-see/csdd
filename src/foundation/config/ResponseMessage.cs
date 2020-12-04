using System.Net;

namespace foundation.config
{
    public class OkMessage
    {
        public dynamic Data { get; } 
        public int Code { get; } = (int)HttpStatusCode.OK;
        public string Msg { get; } = string.Empty;
        public OkMessage(dynamic data) { Data = data; }
        public OkMessage(int code, string msg) { Code = code; Msg = msg; Data = new { }; }
    }
}
