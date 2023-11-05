using System.Text.Json.Serialization;

namespace SendRequestApi.ResponseModels;

public class CompanyModel : BaseModel
{
    [JsonConstructor]
    public CompanyModel(
        int companyId,
        string companyName,
        string companyAddress,
        string companyStatus
    )
    {
        CompanyId = companyId;
        CompanyName = companyName;
        CompanyAddress = companyAddress;
        CompanyStatus = companyStatus;
    }

    [JsonPropertyName("company_id")]
    public int CompanyId { get; }

    [JsonPropertyName("company_name")]
    public string CompanyName { get; }

    [JsonPropertyName("company_address")]
    public string CompanyAddress { get; }

    [JsonPropertyName("company_status")]
    public string CompanyStatus { get; }
}