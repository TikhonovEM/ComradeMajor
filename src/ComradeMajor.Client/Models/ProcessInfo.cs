using ComradeMajor.Interfaces;

namespace ComradeMajor.Models;

public class ProcessInfo : IProcessInfo
{
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        return Name.Equals((obj as IProcessInfo).Name);
    }

    public override int GetHashCode()
    {
        int hashName = Name == null ? 0 : Name.GetHashCode();
        return hashName;
    }
}