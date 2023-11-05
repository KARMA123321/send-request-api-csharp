using System.Text.Json.Serialization;

namespace SendRequestApi.ResponseModels;

public class MetaModel : BaseModel
{
    [JsonConstructor]
    public MetaModel(
        int limit,
        int offset,
        int total
    )
    {
        Limit = limit;
        Offset = offset;
        Total = total;
    }

    [JsonPropertyName("limit")]
    public int Limit { get; }

    [JsonPropertyName("offset")]
    public int Offset { get; }

    [JsonPropertyName("total")]
    public int Total { get; }
}