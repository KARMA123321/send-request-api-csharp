using System.Text.Json.Serialization;

namespace SendRequestApi.ResponseModels;

public class CompaniesModel
{
    [JsonConstructor]
    public CompaniesModel(
        List<CompanyModel> data,
        MetaModel meta
    )
    {
        Data = data;
        Meta = meta;
    }

    [JsonPropertyName("data")]
    public List<CompanyModel> Data { get; }

    [JsonPropertyName("meta")]
    public MetaModel Meta { get; }
}