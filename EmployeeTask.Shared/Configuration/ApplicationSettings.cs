namespace EmployeeTask.Shared.Configuration
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }

    public class HttpOptions
    {
        public string APIBaseUri { get; set; }
    }
}
