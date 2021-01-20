using Flurl;
using Flurl.Http;
using foundation.config;
using System;
using System.Threading.Tasks;

namespace apitest.shared
{
    public class BaseApiTest
    {
        protected static string _rootpath = "http://121.4.51.192:9020";
        private static string _token;
        protected Func<Task<string>> getToken = async () =>
        {
            if (string.IsNullOrEmpty(_token))
            {
                var code = await _rootpath
                    .AppendPathSegment("/api/Token/verification/generate")
                    .SetQueryParam("phone", "+8617775776208")
                    .GetJsonAsync<OkMessage<string>>();
                if (code.Code != 200)
                    throw new Exception(code.Msg);
                var token = await _rootpath
                    .AppendPathSegment("/api/Token")
                    .PostJsonAsync(new { phone = "+8617775776208", code = code.Data })
                    .ReceiveJson<OkMessage<string>>();
                if (token.Code != 200)
                    throw new Exception(token.Msg);
                _token = token.Data;
            }
            return _token;
        };
    }
}
