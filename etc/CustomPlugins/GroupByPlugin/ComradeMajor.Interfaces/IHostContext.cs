using Microsoft.Extensions.Logging;

namespace ComradeMajor.Interfaces;

public interface IHostContext
{
    ILogger Logger { get; set; }
    IStatisticsRepository<IScanResult> Repository { get; set; }
}