using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;

namespace SendRequestApi;

public static class SchemaHelper
{
    private const string SchemeFolderName = "Schemas";
    
    private static OpenApiDocument GetOpenApiSpec(string uri)
    {
        var stream = new RestClient().DownloadStream(new RestRequest(uri));
        return new OpenApiStreamReader().Read(stream, out var diagnostic);
    }

    private static Dictionary<string, string> GetOpenApiScheme(OpenApiDocument openApiSpec)
    {
        var outputDir = Path.Combine(Directory.GetCurrentDirectory(), SchemeFolderName);
        
        var schemas = new Dictionary<string, string>();
        
        foreach (var schemaEntry in openApiSpec.Components.Schemas)
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        
            var outputPath = Path.Combine(outputDir, schemaEntry.Key + ".json");
        
            using var fileStream = new FileStream(outputPath, FileMode.CreateNew);
            var openApiWriterSettings = new OpenApiWriterSettings()
                { InlineLocalReferences = true, InlineExternalReferences = true };
            using var writer = new StreamWriter(fileStream);
            schemaEntry.Value.SerializeAsV3WithoutReference(new OpenApiJsonWriter(writer, openApiWriterSettings));
        }
        
        foreach (var file in Directory.GetFiles(outputDir))
        {
            var fileName = file.Replace(@$"{Directory.GetCurrentDirectory()}\{SchemeFolderName}\", "")
                .Replace(".json", "");
            var schemaText = File.ReadAllText(file);
            schemas[fileName] = schemaText;
        }
        
        Directory.Delete(outputDir, true);

        return schemas;
    }
    
    public static Dictionary<string, string> GetSchemasFromOpenApi(string uri)
    {
        var openApiSpec = GetOpenApiSpec(uri);
        var schemas = GetOpenApiScheme(openApiSpec);

        return schemas;
    }
}