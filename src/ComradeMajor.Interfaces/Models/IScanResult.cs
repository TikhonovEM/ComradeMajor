namespace ComradeMajor.Interfaces;

public interface IScanResult
{
    DateTime Date { get; init; }

    List<IProcessInfo> Processes { get; }
    
}