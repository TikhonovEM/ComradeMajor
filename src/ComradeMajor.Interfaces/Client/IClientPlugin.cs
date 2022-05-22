namespace ComradeMajor.Interfaces.Client;

public interface IClientPlugin
{
    string Identifier { get; set; }
    string Name { get; set; }
    string Description { get; set; }

    void ProcessInfo(IEnumerable<IScanResult> scanResults);
}