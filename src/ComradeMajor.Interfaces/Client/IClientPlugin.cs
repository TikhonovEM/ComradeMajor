namespace ComradeMajor.Interfaces.Client;

public interface IClientPlugin
{
    string Name { get; set; }
    string Description { get; set; }

    void ProcessInfo(IEnumerable<IScanResult> scanResults);
}