namespace ComradeMajor.Interfaces;

public interface IStatisticsRepository<T>
{
    void AddResult(T result);
    void Save();
    IEnumerable<T> GetStatsByDate(DateTime date);
}