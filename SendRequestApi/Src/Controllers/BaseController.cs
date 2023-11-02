namespace SendRequestApi.Controllers;

public abstract class BaseController
{
    private const string BaseUrl = Config.Url;
    protected readonly RestClient Client = new (new RestClientOptions(BaseUrl));
}