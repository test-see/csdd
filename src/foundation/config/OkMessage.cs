using System.Net;

namespace foundation.config
{
    public class OkMessage
    {
        public dynamic Data { get; } 
        public int Code { get; } 
        public string Msg { get; }
        public OkMessage(dynamic data) { Data = data; Code = (int)HttpStatusCode.OK; Msg = string.Empty;}
        public OkMessage(int code, string msg) { Code = code; Msg = msg; Data = new { }; }
    }
}
