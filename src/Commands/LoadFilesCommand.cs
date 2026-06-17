using Cocona;

namespace ConfigurationValidation.Commands;

public class LoadFilesCommand
{
    [Command("loadfiles")]
    public void Load([Argument(Description = "Files to load to workspace")] string[] files)
    {
        var distinctCount = files.Distinct().Count();
        if (distinctCount != files.Length)
        {
            Console.WriteLine("Files have repeated entries.");
            return;
        }
        var workspaceFolder = Path.Combine(Directory.GetCurrentDirectory(), "workspace");
        Directory.CreateDirectory(workspaceFolder);
        foreach (var file in files)
        {
            var ext = Path.GetExtension(file);
            if (ext != ".json" && ext != ".yml" && ext != ".yaml")
            {
                Console.WriteLine($"Cannot load file {file} because it's not in a recognizable format.");
                continue;
            }
            if (!File.Exists(file))
            {
                Console.WriteLine($"File {file} does not exists. Skipping....");
            }

            var copyOfFile = Path.Combine(workspaceFolder, Path.GetFileName(file));
            File.Copy(file, copyOfFile);
        }
    }
}
