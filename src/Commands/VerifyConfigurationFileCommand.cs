using Cocona;
using ConfigurationValidation.Shared.Models;
using ConfigurationValidation.Shared.Utils;

namespace ConfigurationValidation.Commands;

public class VerifyConfigurationFileCommand
{
    [Command("verify")]
    public async Task VerifyAsync([Argument] string file)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "workspace", file);
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File doesn't exist in the workspace yet, please load it in.");
            return;
        }

        var slotConfig = await Parser.FromFileAsync<SlotConfig>(filePath);

        if (slotConfig is null)
        {
            Console.WriteLine("Configuration is not parseable");
            return;
        }

        var errors = new List<string>();
        slotConfig.Validate(ref errors);
        if (errors.Count == 0)
        {
            Console.WriteLine("Configuration valid");
            return;
        }
        Console.WriteLine("======Errors=====");
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }
    }
}

