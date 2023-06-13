using CoreLib.Example;
using CoreLib.RoslynSandbox;
using LeanCode.CQRS.Execution;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;

MSBuildLocator.RegisterDefaults();
using var workspace = MSBuildWorkspace.Create();

var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.Parent!.FullName, "CoreLib.Example.csproj");
var project = await workspace.OpenProjectAsync(path);

// ----------- echo Payment.cs file -----------
//var file = project.Documents.Single(x => x.Name == "Payment.cs");
//var tree = await file.GetSyntaxTreeAsync();
//var root = tree?.GetRoot();

//var walker = new CustomWalker();
//walker.Visit(root);

//Console.WriteLine(walker.Code);

// ----------- lists all CommandHandlers -----------
var compilation = await project.GetCompilationAsync();
if (compilation == null) return;

foreach (var file in project.Documents)
{
    var tree = await file.GetSyntaxTreeAsync();
    var root = tree?.GetRoot();

    var walker = new EventHandlerWalker(compilation.GetSemanticModel(tree));
    walker.Visit(root);

    Console.Write(walker.Code);
}

Console.ReadKey();

