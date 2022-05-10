using ComradeMajor.Interfaces;

namespace ComradeMajor;

public class HostContext : IHostContext
{
    public ILogger Logger { get; set; }
    public IStatisticsRepository<IScanResult> Repository { get; set; }
}