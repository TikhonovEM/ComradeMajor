using ComradeMajor.Interfaces.Client;
using ComradeMajor.Interfaces;

namespace MyPlugin;

public class MyClientPlugin : IClientPlugin
{
     // Must be equal with one of Agent plugin identifiers, to get his stats from DB.
    public string Identifier { get; set; } = nameof(MyAgentPlugin);
    public string Name { get; set; } = "<Plugin Display Name>";
    public string Description { get; set; } = "<My Description>";

    public void ProcessInfo(IEnumerable<IScanResult> scanResults)
    {
        // Do smth
    }
}
