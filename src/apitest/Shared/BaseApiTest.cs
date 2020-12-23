using Flurl;
using Flurl.Http;
using foundation.config;
using System;
using System.Threading.Tasks;

namespace apitest.Shared
{
    public class BaseApiTest
    {
        protected static string _rootpath = "http://121.4.51.192:9010";
        private static string _token;
        protected Func<Task<string>> getToken = async () =>
        {
            if (string.IsNullOrEmpty(_token))
            {
                var code = await _rootpath
                    .AppendPathSegment("/api/Token/verification/generate")
                    .SetQueryParam("phone", "+8617775776208")
                    .GetJsonAsync<OkMessage<string>>();
                var token = await _rootpath
                    .AppendPathSegment("/api/Token")
                    .PostJsonAsync(new { phone = "+8617775776208", code = code.Data })
                    .ReceiveJson<OkMessage<string>>();
                _token = token.Data;
            }
            return _token;
        };
    }
}
