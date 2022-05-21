using ComradeMajor.Interfaces;
using Newtonsoft.Json;

namespace ComradeMajor.Models;

public class ScanResult : IScanResult
{
    public DateTime Date { get; init; }

    public IProcessInfo ActiveProcess { get; set; }

    public List<IProcessInfo> Processes { get; }

    public ScanResult()
    {
        Processes = new List<IProcessInfo>();
    }

    public override string ToString()
    {
        return $"('{Date.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{JsonConvert.SerializeObject(ActiveProcess)}', '{JsonConvert.SerializeObject(Processes)}')";
    }
    
}