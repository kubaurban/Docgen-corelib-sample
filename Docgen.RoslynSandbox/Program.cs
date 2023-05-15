using CoreLib.RoslynSandbox;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;

MSBuildLocator.RegisterDefaults();
using var workspace = MSBuildWorkspace.Create();

var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName, "CoreLib.Example.csproj");
var project = await workspace.OpenProjectAsync(path);

var file = project.Documents.Single(x => x.Name == "Payment.cs");
var tree = await file.GetSyntaxTreeAsync();
var root = tree?.GetRoot();

var walker = new CustomWalker();
walker.Visit(root);

Console.WriteLine(walker.Code);
Console.ReadKey();
