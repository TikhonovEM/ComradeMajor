namespace ComradeMajor.Interfaces;

public interface IScanResult
{
    string PluginIdentifier { get; set; }
    int ScanPeriod { get; set; }
    DateTime Date { get; set; }
    IProcessInfo ActiveProcess { get; set;}

    List<IProcessInfo> Processes { get; set; }
    
}