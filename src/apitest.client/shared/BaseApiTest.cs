using Flurl;
using Flurl.Http;
using foundation.config;
using System;
using System.Threading.Tasks;

namespace apitest.shared
{
    public class BaseApiTest
    {
        protected static string _rootpath = "http://121.4.51.192:9030";
        private static string _token;
        private static string _smscode = "";
        protected Func<Task<string>> getToken = async () =>
        {
            if (string.IsNullOrEmpty(_smscode))
            {
                var code = await _rootpath
                    .AppendPathSegment("/api/Token/verification/generate")
                    .SetQueryParam("phone", "17777777777")
                    .GetJsonAsync<OkMessage<string>>();
                if (code.Code != 200)
                    throw new Exception(code.Msg);
                _smscode = code.Data;
            }
            if (string.IsNullOrEmpty(_token))
            {
                var token = await _rootpath
                    .AppendPathSegment("/api/Token")
                    .PostJsonAsync(new { phone = "17777777777", code = _smscode })
                    .ReceiveJson<OkMessage<string>>();
                if (token.Code != 200)
                    throw new Exception(token.Msg);
                _token = token.Data;
            }
            return _token;
        };
    }
}
