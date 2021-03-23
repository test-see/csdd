using foundation.exception;
using System.Linq;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Sms.V20190711;
using TencentCloud.Sms.V20190711.Models;

namespace foundation._3party
{
    public class SmsSendRequest
    {
        private static readonly string _endpoint = "sms.tencentcloudapi.com";
        private static readonly string _verificationCodeTemplateId = "880929";
        private static readonly string _appId = "1400216329";
        private static readonly string _sign= "徙木信息";
        private readonly Credential _cred;

        public SmsSendRequest(Credential cred)
        {
            _cred = cred;
        }

        private async Task SendAsync(string[] phone, string templateId, string[] param)
        {
            ClientProfile clientProfile = new ClientProfile();
            HttpProfile httpProfile = new HttpProfile();
            httpProfile.Endpoint = (_endpoint);
            clientProfile.HttpProfile = httpProfile;
            SmsClient client = new SmsClient(_cred, "", clientProfile);
            SendSmsRequest req = new SendSmsRequest();
            req.PhoneNumberSet = phone;
            req.TemplateID = templateId;
            req.SmsSdkAppid = _appId;
            req.TemplateParamSet = param;
            req.Sign = _sign;
            var result = await client.SendSms(req);
            var message = result.SendStatusSet.Any(x => x.Code == "Ok");
            if (!message) throw new DefaultException(result.SendStatusSet.LastOrDefault()?.Message);
        }

        public async Task SendVerificationCodeAsync(string[] phone, string param)
        {
            await SendAsync(phone.Select(x => "+86" + x).ToArray(), _verificationCodeTemplateId, new string[] { param });
        }
    }
}
