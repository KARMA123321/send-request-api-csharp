namespace SendRequestApi.Controllers;

public abstract class BaseController
{
    private const string BaseUrl = "https://send-request.me/api";
    protected readonly RestClient Client = new (new RestClientOptions(BaseUrl));
}