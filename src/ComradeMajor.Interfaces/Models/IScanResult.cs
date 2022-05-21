namespace ComradeMajor.Interfaces;

public interface IScanResult
{
    DateTime Date { get; init; }
    IProcessInfo ActiveProcess { get; set;}

    List<IProcessInfo> Processes { get; }
    
}