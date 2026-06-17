using Cocona;
using ConfigurationValidation.Shared.Models;
using ConfigurationValidation.Shared.Utils;

namespace ConfigurationValidation.Commands;

public class CompareFilesCommand
{
    [Command("compare")]
    public async Task CompareFilesAsync([Argument] string file1, [Argument] string file2)
    {
        var file1Path = Path.Combine(Directory.GetCurrentDirectory(), "workspace", file1);
        if (!File.Exists(file1Path))
        {
            Console.WriteLine($"File {file1} doesn't exist in the workspace yet, please load it in.");
            return;
        }

        var file2Path = Path.Combine(Directory.GetCurrentDirectory(), "workspace", file2);
        if (!File.Exists(file2Path))
        {
            Console.WriteLine($"File {file2} doesn't exist in the workspace yet, please load it in.");
            return;
        }

        var config1 = await Parser.FromFileAsync<SlotConfig>(file1Path);
        var config2 = await Parser.FromFileAsync<SlotConfig>(file2Path);

        // compare configuration
    }
}
