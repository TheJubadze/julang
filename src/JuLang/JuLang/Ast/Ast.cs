namespace JuLang;

public interface INode {
    string TokenLiteral();
}

public interface IStatement : INode {
    void StatementNode();
}

public interface IExpression : INode {
    void ExpressionNode();
}

public class Program : INode {
    public List<IStatement> Statements { get; set; } = new List<IStatement>();

    public string TokenLiteral() {
        return Statements.Count > 0 ? Statements[0].TokenLiteral() : string.Empty;
    }
}