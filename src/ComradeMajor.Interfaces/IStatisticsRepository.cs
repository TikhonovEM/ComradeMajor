namespace ComradeMajor.Interfaces;

public interface IStatisticsRepository<T>
{
    void AddResult(T result);
    void Save();
    Task SaveAsync();
    IEnumerable<T> GetStatsByDate(DateTime date);
}