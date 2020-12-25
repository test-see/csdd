namespace foundation.config
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public string Version { get; set; }
    }
    public class AuthenticationConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
    }
}
