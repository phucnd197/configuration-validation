using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ConfigurationValidation.Shared.Utils;

internal sealed class Parser
{
    private readonly static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
    public static async Task<T?> FromFileAsync<T>(string path)
    {
        var extension = Path.GetExtension(path);
        switch (extension)
        {
            case ".yaml":
            case ".yml":
                {
                    using var reader = new StreamReader(path);
                    var yamlDeserializer = new DeserializerBuilder()
                        .WithNamingConvention(PascalCaseNamingConvention.Instance)
                        .Build();
                    return yamlDeserializer.Deserialize<T>(reader);
                }
                break;
            case ".json":
                {
                    using var stream = File.OpenRead(path);
                    return await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions);
                }
                break;
            default:
                Console.WriteLine("File not in recognizable format, please use file in json or yaml");
                return default;
        }
    }
}
