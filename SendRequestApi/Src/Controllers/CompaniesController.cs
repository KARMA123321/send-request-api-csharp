using SendRequestApi.ResponseModels;

namespace SendRequestApi.Controllers;

public class CompaniesController : BaseController
{
    private const string Url = "/companies";

    public RestResponse<CompaniesModel> GetCompanies(string? status = null, int? limit = null, int? offset = null)
    {
        var request = new RestRequest(Url);
        if (status is not null) request.AddParameter("status", status);
        if (limit is not null) request.AddParameter("limit", (int)limit);
        if (offset is not null) request.AddParameter("offset", (int)offset);
        
        return Client.ExecuteGet<CompaniesModel>(request);
    }

    public RestResponse<CompanyModel> GetCompanyById()
    {
        throw new NotImplementedException();
    }
}