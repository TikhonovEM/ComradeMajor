using ComradeMajor.Interfaces;
using Newtonsoft.Json;

namespace ComradeMajor.Models;

public class ScanResult : IScanResult
{
    public string PluginIdentifier { get; set; }
    public DateTime Date { get; set; }

    public IProcessInfo ActiveProcess { get; set; }

    public List<IProcessInfo> Processes { get; set; }

    public ScanResult()
    {
        Processes = new List<IProcessInfo>();
    }

    public override string ToString()
    {
        return $"('{PluginIdentifier}', '{Date.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{JsonConvert.SerializeObject(ActiveProcess)}', '{JsonConvert.SerializeObject(Processes)}')";
    }
    
}