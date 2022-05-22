namespace ComradeMajor.Interfaces.Agent;

public interface IAgentPlugin
{
    string Identifier { get; set; }
    void Execute(IHostContext context);
}