using ComradeMajor.Interfaces;

namespace ComradeMajor.Models;

public class ScanResult : IScanResult
{
    public string PluginIdentifier { get; set; }
    public int ScanPeriod { get; set; }
    public DateTime Date { get; set; }

    public IProcessInfo ActiveProcess { get; set; }

    public List<IProcessInfo> Processes { get; set; }

    public ScanResult()
    {
        Processes = new List<IProcessInfo>();
    }
    
}