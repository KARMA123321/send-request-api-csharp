namespace SendRequestApi;

public class Config
{
    public const string BaseProtocol = "https";
    public const string BaseUrl = "send-request.me";
    public const string BasePath = "api";
    public const string Url = $"{BaseProtocol}://{BaseUrl}/{BasePath}";
    public const string OpenApiSpecUrl = $"{Url}/openapi.json";
}