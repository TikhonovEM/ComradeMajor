using ComradeMajor.Interfaces.Agent;
using ComradeMajor.Interfaces;

namespace MyPlugin;

public class MyAgentPlugin : IScanPlugin
{
    public string Identifier { get; set; } = nameof(MyAgentPlugin);

    public void Execute(IHostContext context)
    {
        // Do smth
    }
}
