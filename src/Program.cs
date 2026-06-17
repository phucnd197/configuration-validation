using Cocona;
using ConfigurationValidation.Commands;
using ConfigurationValidation.Models;
using System.Text.Json;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommands<LoadFilesCommand>();


app.AddCommand("verify",);


app.Run();