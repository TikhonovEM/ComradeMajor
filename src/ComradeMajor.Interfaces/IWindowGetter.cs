namespace ComradeMajor.Interfaces;

public interface IWindowGetter
{
    IEnumerable<IProcessInfo> GetOpenedWindows();

    IProcessInfo GetActiveWindow();
}