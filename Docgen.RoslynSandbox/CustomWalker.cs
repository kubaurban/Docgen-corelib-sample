using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;

namespace CoreLib.RoslynSandbox;

public class CustomWalker : CSharpSyntaxWalker
{
    private readonly StringBuilder _sb;
    public string Code => _sb.ToString();

    public CustomWalker()
        : base(SyntaxWalkerDepth.Token)
    {
        _sb = new StringBuilder();
    }

    public override void VisitToken(SyntaxToken token)
    {
        _sb.Append(token.ToFullString());

        base.VisitToken(token);
    }
}
