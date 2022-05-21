using ComradeMajor.Interfaces;

namespace ComradeMajor;

public class LinuxWindowGetter : IWindowGetter
{
    public IEnumerable<IProcessInfo> GetOpenedWindows()
    {
        var processInfos = new List<IProcessInfo>();
        return processInfos;

    }

    public IProcessInfo GetActiveWindow()
    {
        return null;
    }
}