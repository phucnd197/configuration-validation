using Cocona;
using ConfigurationValidation.Commands;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommands<LoadFilesCommand>();
app.AddCommands<VerifyConfigurationFileCommand>();
app.AddCommands<CompareFilesCommand>();

app.Run();