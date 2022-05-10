using ComradeMajor.Interfaces;

namespace ComradeMajor.Models;

public class ScanResult : IScanResult
{
    public DateTime Date { get; init; }

    public List<IProcessInfo> Processes { get; }

    public ScanResult()
    {
        Processes = new List<IProcessInfo>();
    }
    
}