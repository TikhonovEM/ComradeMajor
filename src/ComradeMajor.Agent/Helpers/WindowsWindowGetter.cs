using ComradeMajor.Interfaces;
using ComradeMajor.Models;
using System.Runtime.InteropServices;
using System.Text;
using HWND = System.IntPtr;

namespace ComradeMajor;

public class WindowsWindowGetter : IWindowGetter
{
    private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

    [DllImport("USER32.DLL")]
    private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowTextLength(HWND hWnd);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(HWND hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
    public IEnumerable<IProcessInfo> GetOpenedWindows()
    {
        var processInfos = new List<IProcessInfo>();
        var shellWindow = GetShellWindow();

        EnumWindows(delegate(HWND hWnd, int lParam)
        {
            if (hWnd == shellWindow) return true;
            if (!IsWindowVisible(hWnd)) return true;

            int length = GetWindowTextLength(hWnd);
            if (length == 0) return true;

            var builder = new StringBuilder(length);
            GetWindowText(hWnd, builder, length + 1);

            var pi = new ProcessInfo();
            pi.Name = builder.ToString();
            processInfos.Add(pi);
            return true;

        }, 0);

        return processInfos;

    }

    public IProcessInfo GetActiveWindow()
    {
        const int nChars = 256;
        StringBuilder buff = new StringBuilder(nChars);
        IntPtr handle = GetForegroundWindow();

        if (GetWindowText(handle, buff, nChars) > 0)
        {
            return new ProcessInfo()
            {
                Name = buff.ToString()
            };
        }
        return null;
    }
}