using LeanCode.CQRS.Execution;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace CoreLib.RoslynSandbox;

public class EventHandlerWalker : CSharpSyntaxWalker
{
    private readonly StringBuilder _sb;
    private readonly SemanticModel _model;

    public string Code => _sb.ToString();

    public EventHandlerWalker(SemanticModel model)
    {
        _model = model;
        _sb = new StringBuilder();
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var symbol = _model.GetDeclaredSymbol(node);
        
        if (symbol is not null && symbol.Interfaces.Any(x => x.MetadataName == typeof(ICommandHandler<,>).Name))
        {
            _sb.AppendLine(symbol.Name);
        }

        base.VisitClassDeclaration(node);
    }
}
