namespace foundation.config
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public TencentCloudSmsConfig TencentCloudSMS { get; set; }
        public RabbitMqConfig RabbitMq { get; set; }
    }
    public class AuthenticationConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
    }
    public class TencentCloudSmsConfig
    {
        public string SecretId { get; set; }
        public string SecretKey { get; set; }
    }
    public class RabbitMqConfig
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
