using NJsonSchema;
using SendRequestApi.Enums;
using SendRequestApi.Tests;

namespace SendRequestApi.Asserts;

public abstract class SchemaAssert
{
    public static void Matches(string? responseContent, JsonSchemaType schema, 
        string errorMessage = "Schema validation ended with errors")
    {
        if (responseContent is null) 
            throw new ArgumentNullException(nameof(responseContent), "Response content is null");
        
        var errors = JsonSchema.FromJsonAsync(TestsSetUp.Schemas?[schema.ToString()])
            .Result.Validate(responseContent, SchemaType.OpenApi3);
        
        Assert.That(errors, Is.Empty, errorMessage);
    }
}